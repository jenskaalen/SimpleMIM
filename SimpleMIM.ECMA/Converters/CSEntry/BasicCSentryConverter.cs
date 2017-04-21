using System;
using System.Collections.Generic;
using Microsoft.MetadirectoryServices;
using SimpleMIM.ECMA.Converters.Value;

namespace SimpleMIM.ECMA.Converters.CSEntry
{
    internal class BasicCSentryConverter<T> : ICSentryConverter<T> where T : IExternalObject
    {
        private readonly IPropertyValueConverter _valueConverter;

        internal BasicCSentryConverter(IPropertyValueConverter valueConverter)
        {
            _valueConverter = valueConverter;
        }

        public T ConvertFromCSentry(CSEntryChange csentry)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            Type type = typeof (T);

            foreach (AttributeChange attribute in csentry.AttributeChanges)
            {
                var entityProperty = type.GetProperty(attribute.Name);

                object value = _valueConverter.GetEntityPropertyValue(attribute);
                entityProperty.SetValue(obj, value);
            }

            throw new System.NotImplementedException();
        }

        public List<T> ConvertFromCSentries(List<CSEntryChange> csentry)
        {
            throw new System.NotImplementedException();
        }

        public CSEntryChange ConvertToCSentry(T entity)
        {
            throw new System.NotImplementedException();
        }

        public List<CSEntryChange> ConvertToCSentries(List<T> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}