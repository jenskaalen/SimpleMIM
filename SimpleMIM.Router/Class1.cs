using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Router
{
    public class Router : IMVSynchronization
    {
        private List<IMVSynchronization> _loadedDlls = new List<IMVSynchronization>();
        public void Initialize()
        {
            //should contain one line per extension assembly, e.g. "MyProvisionCode.dll" 
            string mvRouterFile = Path.Combine(Utils.WorkingDirectory, "provisioningAssemblies.txt");
            //will throw error exceptio if the file is not found
            string[] filenames = File.ReadAllLines(mvRouterFile);
            LoadAssemblies(filenames);
        }

        private void LoadAssemblies(string[] filenames)
        {
            foreach (var filename in filenames)
            {
                string completePath = Path.Combine(Utils.ExtensionsDirectory, filename);
                Assembly assem = Assembly.LoadFrom(completePath);
                Type[] types = assem.GetExportedTypes();
                IMVSynchronization provisionDll = null;

                foreach (Type type in types)
                {
                    if (type.GetInterface("Microsoft.MetadirectoryServices.IMVSynchronization") != null)
                    {
                        provisionDll = (IMVSynchronization)assem.CreateInstance(type.FullName);
                    }
                }

                // If an object type starting with "MVExtension" could not be found,
                // or if an instance could not be created, throw an exception.
                if (provisionDll == null)
                {
                    throw new Exception("Unable to load file " + completePath + " as an provisioning assembly (IMVSynchronization)");
                }

                _loadedDlls.Add(provisionDll);

                // Call the Initialize() method on each MV extension object.
                provisionDll.Initialize();
            }
        }

        public void Terminate()
        {
            foreach (var dll in _loadedDlls)
            {
                dll.Terminate();
            }
        }

        public void Provision(MVEntry mventry)
        {
            foreach (var dll in _loadedDlls)
            {
                dll.Provision(mventry);
            }
        }

        public bool ShouldDeleteFromMV(CSEntry csentry, MVEntry mventry)
        {
            bool shouldDelete = _loadedDlls.All(dll => dll.ShouldDeleteFromMV(csentry, mventry));
            return shouldDelete;
        }
    }
}