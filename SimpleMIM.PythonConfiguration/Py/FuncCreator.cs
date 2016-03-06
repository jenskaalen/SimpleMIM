using System;

namespace SimpleMIM.PythonConfiguration.Py
{
    public class FuncCreator
    {
        /// <summary>
        /// Generates a python function 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variablesString">Ex "varA, varB"</param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GenerateFunction(string name, string variablesString, string expression)
        {
            const string multilineFuncTemplate =
                "def {0}({1}):\n{2}";
            return String.Format(multilineFuncTemplate, name, variablesString, expression)
                .Replace("\n", "\n\t");
    //        if (expression.IndexOf("\n", StringComparison.Ordinal) > -1)
    //        {
    //            const string multilineFuncTemplate =
    //                "def {0}({1}):\n{2}";
    //            return String.Format(multilineFuncTemplate, name, variablesString, expression)
    //                .Replace("\n", "\n\t");
    //        }
    //        else
    //        {
    //            const string funcTemplate =
    //@"def {0}({1}):
    //return {2}";

    //            return String.Format(funcTemplate, name, variablesString, expression);
    //        }
        }
    }
}
