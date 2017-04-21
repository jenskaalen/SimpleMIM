using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.ECMA.SchemaMapping
{
    public class AutoMapper
    {
        public static SchemaType GetSchema(Type externalObjectType, string schemaObjectName)
        {
            PropertyInfo[] properties = externalObjectType.GetProperties();
            SchemaType schemaType = SchemaType.Create(schemaObjectName, false);

            foreach (PropertyInfo propertyInfo in properties)
            {
                var attrib = GetSchemaAttribute(schemaObjectName, propertyInfo.PropertyType);

                schemaType.Attributes.Add(
                    attrib
                    );
            }

            return schemaType;
        }

        private static SchemaAttribute GetSchemaAttribute(string name, Type type)
        {
            SchemaAttribute schemaAttribute;
            bool isMultivalued = typeof (IEnumerable).IsAssignableFrom(type);

            if (isMultivalued)
                schemaAttribute = SchemaAttribute.CreateMultiValuedAttribute(name, GetSchemaAttributeType(type));
            else
                schemaAttribute = SchemaAttribute.CreateSingleValuedAttribute(name, GetSchemaAttributeType(type));

            return schemaAttribute;
        }

        private static AttributeType GetSchemaAttributeType(Type type)
        {
            if (type == typeof(string))
            {
                return AttributeType.String;
            }

            if (type == typeof(bool))
            {
                return AttributeType.Boolean;
            }

            if (type == typeof(int))
            {
                return AttributeType.Integer;
            }

            if (type == typeof(Guid))
                return AttributeType.Reference;

            //fallback is a json string
            return AttributeType.String;
        }
    }
}
