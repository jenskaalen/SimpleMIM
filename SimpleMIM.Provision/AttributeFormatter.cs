using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Provision
{
    public static class AttributeFormatter
    {
        public static string FormatValue(MVEntry mventry, string replaceFormat)
        {
            string generatedValue = replaceFormat;
            //This regex will catch any bracketed attribute names in pattern
            var parameterRegex = new Regex(@"\[(.*?)\]");
            var matches = parameterRegex.Matches(replaceFormat);

            var replacePatterns = new List<string>();

            for (int i = 0; i < matches.Count; i++)
                replacePatterns.Add(matches[i].Value);

            foreach (string replacePattern in replacePatterns)
            {
                string attributeName = replacePattern.Trim('[', ']');
                generatedValue = generatedValue.Replace(replacePattern, mventry[attributeName].Value);
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