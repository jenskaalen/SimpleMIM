using System;

namespace SimpleMIM.PythonConfiguration.Py
{
    public class FuncCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variablesString">Ex "varA, varB"</param>
        /// <param name="oneLineExpression"></param>
        /// <returns></returns>
        public static string GenerateFunction(string name, string variablesString, string oneLineExpression)
        {
            const string funcTemplate =
@"def {0}({1}):
    return {2}";

            return String.Format(funcTemplate, name, variablesString, oneLineExpression);
        }
    }
}
