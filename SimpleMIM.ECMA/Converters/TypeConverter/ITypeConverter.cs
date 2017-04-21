using System;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.ECMA.Converters.TypeConverter
{
    interface ITypeConverter
    {
        Type GetType(AttributeType attribute);
        AttributeType GetAttribute(Type type);
    }
}
