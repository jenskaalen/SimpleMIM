using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.MetadirectoryServices;
using SimpleMIM.ECMA.SchemaMapping;

namespace SimpleMIM.ECMA
{
    public abstract class BaseEcma:
        IMAExtensible2CallExport,
        IMAExtensible2CallImport,
        IMAExtensible2GetSchema,
        IMAExtensible2GetCapabilities,
        IMAExtensible2GetParameters
    {
        public int ExportDefaultPageSize
        {
            get { return 12; }
        }

        public int ExportMaxPageSize
        {
            get { return 50; }
        }

        public int ImportDefaultPageSize
        {
            get { return 50; }
        }

        public int ImportMaxPageSize
        {
            get { return 50; }
        }

        public MACapabilities Capabilities
        {
            get { return GetMaCapabilities(); }
        }


        private const int MinImportPageSize = 5;

        public virtual int ImportPageSize
        {
            get { return _setImportPageSize.Clamp(MinImportPageSize, ImportMaxPageSize); }
        }

        protected List<IObjectSource<IExternalObject>> _objectSources;
        private int _entitesImportedCount;
        private int _setImportPageSize;
        private int _currentRepoIndex;
        private Schema _schemaTypes;
        private Dictionary<IObjectSource<IExternalObject>, List<IExternalObject>> _objectImportsDictionary;

        public void OpenExportConnection(KeyedCollection<string, ConfigParameter> configParameters, Schema types,
            OpenExportConnectionRunStep exportRunStep)
        {
            _schemaTypes = types;
            _objectSources = GetRepositoryContainers(configParameters);
        }
        
        public PutExportEntriesResults PutExportEntries(IList<CSEntryChange> csentries)
        {
            var results = new PutExportEntriesResults();

            foreach (var csentry in csentries)
            {
                try
                {
                    //Logger.Log.DebugFormat("Exporting csentry {0} with modificationType {1}", csentry.DN,
                    //          csentry.ObjectModificationType);

                    //Generic type will always be named the same as the csentry objecttype
                    string typeName = csentry.ObjectType;
                    IObjectSource<IExternalObject> repoContainer = _objectSources.FirstOrDefault(source => source.ObjectTypeName == typeName);

                    if (repoContainer == null)
                        throw new Exception("Couldnt find RepoContainer for type " + typeName);

                    //object entity = typeof(CsentryConverter)
                    //    .GetMethod("ConvertFromCsentry")
                    //    .MakeGenericMethod(new Type[] { repoContainer.Type })
                    //    .Invoke(null, new[] { csentry });
                    IExternalObject entity = repoContainer.CSentryConverter.ConvertFromCSentry(csentry);

                    if (entity == null)
                    {
                        string errorMsg = String.Format("Was not able to convert CSEntry to {0}",
                            repoContainer.ObjectTypeName);

                        throw new Exception(errorMsg);
                    }

                    switch (csentry.ObjectModificationType)
                    {
                        case ObjectModificationType.Add:
                            repoContainer.Add(entity);
                            break;
                        case ObjectModificationType.Delete:
                            repoContainer.Delete(entity);
                            break;
                        case ObjectModificationType.Replace:
                        case ObjectModificationType.Update:
                            repoContainer.Update(entity);
                            break;
                        case ObjectModificationType.Unconfigured:
                            break;
                        case ObjectModificationType.None:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var changeResult = CSEntryChangeResult.Create(csentry.Identifier, null,
                        MAExportError.ExportErrorCustomContinueRun
                        , "script-error", ex.ToString());
                    results.CSEntryChangeResults.Add(changeResult);
                }
            }

            return results;
        }

        public void CloseExportConnection(CloseExportConnectionRunStep exportRunStep)
        {
            foreach (var repoContainer in _objectSources)
            {
                repoContainer.Dispose();
            }
        }

        public OpenImportConnectionResults OpenImportConnection(
            KeyedCollection<string, ConfigParameter> configParameters, Schema types,
            OpenImportConnectionRunStep importRunStep)
        {
            _schemaTypes = types;
            _objectSources = GetRepositoryContainers(configParameters);
            
            _setImportPageSize = importRunStep.PageSize > 0 ? importRunStep.PageSize : ImportDefaultPageSize;
            
            var importResults = new OpenImportConnectionResults();
            _objectImportsDictionary = new Dictionary<IObjectSource<IExternalObject>, List<IExternalObject>>();

            foreach (IObjectSource<IExternalObject> repoContainer in _objectSources)
            {
                List<IExternalObject> allExternalObjects =  repoContainer.GetAll();
                _objectImportsDictionary.Add(repoContainer, allExternalObjects);
            }

            return importResults;
        }

        public GetImportEntriesResults GetImportEntries(GetImportEntriesRunStep importRunStep)
        {
            IObjectSource<IExternalObject> objectSource = _objectSources[_currentRepoIndex];
            List<IExternalObject> importedObjects = _objectImportsDictionary[objectSource];

            List<IExternalObject> currentImportBatchEntities = importedObjects
                .Skip(_entitesImportedCount)
                .Take(ImportPageSize)
                .ToList();

            _entitesImportedCount += currentImportBatchEntities.Count;

            List<CSEntryChange> csentries = objectSource.CSentryConverter.ConvertToCSentries(currentImportBatchEntities);
            var importInfo = new GetImportEntriesResults { MoreToImport = true };

            if (_entitesImportedCount >= importedObjects.Count)
                importInfo.MoreToImport = false;

            //switching to next repository
            if (importInfo.MoreToImport == false)
            {
                if (_objectSources.Count > _currentRepoIndex + 1)
                {
                    _currentRepoIndex++;
                    importInfo.MoreToImport = true;
                    _entitesImportedCount = 0;
                }
            }

            importInfo.CSEntries = csentries;
            return importInfo;
        }

        //private List<CSEntryChange> ConvertToCsentries(List<object> currentImportBatchEntities)
        //{
        //    //TODO: throw error or warning?
        //    if (currentImportBatchEntities.Count == 0)
        //        return new List<CSEntryChange>();

        //    var contactCsentries = new List<CSEntryChange>();
        //    string typeName = currentImportBatchEntities.First().GetType().Name;

        //    List<string> usedAttributes = _schemaTypes.Types[typeName].Attributes
        //        .Select(attribute => attribute.Name).ToList();

        //    if (_schemaTypes.Types[typeName].AnchorAttributes.Any())
        //    {
        //        usedAttributes.AddRange(_schemaTypes.Types[typeName].AnchorAttributes.Select(attribute => attribute.Name));
        //    }

        //    foreach (object contact in currentImportBatchEntities)
        //    {
        //        CSEntryChange csentry = CSEntryChange.Create();
        //        //TODO: change csentryconverter to automatically map name from type name. Setting of ObjectType can then be removed
        //        csentry.ObjectType = typeName;
        //        csentry.ObjectModificationType = ObjectModificationType.Add;

        //        try
        //        {
        //            //List<AttributeChange> attributes = CsentryConverter.GetCsentryAttributes(contact);
        //            List<AttributeChange> attributes = CsentryConverter.GetCsentryAttributes(contact, usedAttributes);

        //            attributes.ForEach(atr => csentry.AttributeChanges.Add(atr));
        //            contactCsentries.Add(csentry);
        //        }
        //        catch (Exception ex)
        //        {
        //            csentry.ErrorCodeImport = MAImportError.ImportErrorCustomContinueRun;
        //            csentry.ErrorDetail = ex.ToString();
        //            Logger.Log.Error(ex);
        //        }
        //    }

        //    return contactCsentries;
        //}

        public virtual CloseImportConnectionResults CloseImportConnection(CloseImportConnectionRunStep importRunStep)
        {
            foreach (var repoContainer in _objectSources)
            {
                repoContainer.Dispose();
            }
            
            return new CloseImportConnectionResults();
        }

        public virtual Schema GetSchema(KeyedCollection<string, ConfigParameter> configParameters)
        {
            //TODO: need to pass configparameters in here
            _objectSources = GetRepositoryContainers(configParameters);
            Schema schema = Schema.Create();

            foreach (var objectSource in _objectSources)
            {
                SchemaType schemaType = AutoMapper.GetSchema(objectSource.Type, objectSource.ObjectTypeName);
                schema.Types.Add(schemaType);
            }

            return schema;
        }

        public IList<ConfigParameterDefinition> GetConfigParameters(
            KeyedCollection<string, ConfigParameter> configParameters, ConfigParameterPage page)
        {
            var configParametersDefinitions = new List<ConfigParameterDefinition>();

            if (page == ConfigParameterPage.Global)
                configParametersDefinitions.Add(ConfigParameterDefinition.CreateCheckBoxParameter("Use mockup data"));

            IList<ConfigParameterDefinition> customParameters = GetEzmaConfigParameters(configParameters, page);

            if (customParameters != null)
                configParametersDefinitions.AddRange(customParameters);

            //NOTE: need this because ezmas are seemingly requierd to have at least one connection attribute
            if (page == ConfigParameterPage.Connectivity && configParametersDefinitions.Count == 0)
                configParametersDefinitions.Add(ConfigParameterDefinition.CreateStringParameter("RequiredEmptyParameter", null, "IgnoreThisValue"));

            return configParametersDefinitions;
        }

        public abstract IList<ConfigParameterDefinition> GetEzmaConfigParameters(
            KeyedCollection<string, ConfigParameter> configParameters, ConfigParameterPage page);

        public virtual ParameterValidationResult ValidateConfigParameters(
            KeyedCollection<string, ConfigParameter> configParameters, ConfigParameterPage page)
        {
            return new ParameterValidationResult();
        }

        //private MACapabilities GetMaCapabilities()
        //{
        //    return CapabilityLoader.LoadCapabilities(Path.Combine(MaConfigLocation, MaCapabilitiesFilename));
        //}

        protected abstract List<IObjectSource<IExternalObject>> GetRepositoryContainers(KeyedCollection<string, ConfigParameter> configParameters);

        protected virtual MACapabilities GetMaCapabilities()
        {
            return new MACapabilities()
            {
                ConcurrentOperation = true,
                DeleteAddAsReplace = false,
                DeltaImport = false,
                DistinguishedNameStyle = MADistinguishedNameStyle.None,
                ExportType = MAExportType.ObjectReplace,
                NoReferenceValuesInFirstExport = true,
                ObjectRename = false
            };
        }
    }
}
