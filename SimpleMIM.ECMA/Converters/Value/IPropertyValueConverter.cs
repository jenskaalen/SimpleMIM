using Microsoft.MetadirectoryServices;

namespace SimpleMIM.ECMA.Converters.Value
{
    interface IPropertyValueConverter
    {
        object GetCSEntryValue(object originalValue);
        object GetEntityPropertyValue(AttributeChange change);
    }
}
