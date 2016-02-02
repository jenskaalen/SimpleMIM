using System;
using System.Collections.Generic;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Provision
{
    public static class AttributeFormatter
    {
        public static string FormatAttribute(string replaceFormat, params Attrib[] attributes)
        {
            string generatedValue = replaceFormat;

            foreach (Attrib attribute in attributes)
            {
                string replaceText = String.Format("[{0}]", attribute.Name);
                generatedValue = generatedValue.Replace(replaceText, attribute.Value);
            }

            return generatedValue;
        }

        //public static string FormatAttribute(string replaceFormat, IEnumerable<Attrib> attributes)
        //{
        //    string generatedValue = replaceFormat;

        //    foreach (Attrib attribute in attributes)
        //    {
        //        string replaceText = String.Format("[{0}]", attribute.Name);
        //        generatedValue = generatedValue.Replace(replaceText, attribute.Value);
        //    }

        //    return generatedValue;
        //}
    }
}