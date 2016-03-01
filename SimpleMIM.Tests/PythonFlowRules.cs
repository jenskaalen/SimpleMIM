using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MetadirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIMSimplifier.Tests.MockTypes;
using SimpleMIM.Flow;
using SimpleMIM.Flow.RuleLoading;
using SimpleMIM.PythonConfiguration.Py;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class PythonFlowRules
    {
        private const string testFuncName = "testFunc";
        private const string testExpression = "x + 2";
        private const string testVariables = "x";

        [TestMethod]
        public void Generate_function_string()
        {
            string func = FuncCreator.GenerateFunction(testFuncName, testVariables, testExpression);
            Assert.IsTrue(func.Contains(testFuncName) && func.Contains("return " + testExpression));
        }

        [TestMethod]
        public void Register_and_get_function()
        {
            string funcScript = FuncCreator.GenerateFunction(testFuncName, testVariables, testExpression);
            Core.RegisterFlowScript(funcScript);

            var func = Core.GetFlowFunction(testFuncName);
            Assert.IsNotNull(func);

            int result = func(4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Dictionary_variable_works_in_function()
        {
            const string expression = "dic['givenName'] + ' lund'";
            var dic = new Dictionary<string, string>();
            dic.Add("givenName", "per");

            string script = FuncCreator.GenerateFunction("dicTest", "dic", expression);
            Core.RegisterFlowScript(script);

            var func = Core.GetFlowFunction("dicTest");
            string name = func(dic);
            Assert.AreEqual("per lund", name);
        }

        [TestMethod]
        public void MIM_attribs_work_in_function()
        {
            MVEntry entry = new MockMventry();
            entry["firstName"].Value = "espen";
            entry["lastName"].Value = "askeladd";
            
            const string expression = "entry['firstName'].Value + ' ' + entry['lastName'].Value";
            string script = FuncCreator.GenerateFunction("entryTest", "entry", expression);
            Core.RegisterFlowScript(script);

            var func = Core.GetFlowFunction("entryTest");
            string name = func(entry);
            Assert.AreEqual("espen askeladd", name);
        }
        
        [TestMethod]
        public void Test_flowrule_from_file()
        {
            CSEntry entry = new MockCsentry();
            entry["FirstName"].Value = "Espen";
            entry["LastName"].Value = "Askeladd";

            var loader = new FileRuleLoader("Samples\\pyFlowRules.json");
            List<FlowRule> rules = loader.LoadRules();

            Assert.IsTrue(rules.Any(rule => rule.Name == "UpperCaser"));
            var upperCaseRule = rules.FirstOrDefault(rule => rule.Name == "UpperCaser");

            string uppercasedName = RuleEval.GetValue(upperCaseRule, entry);
            Assert.AreEqual("ESPEN ASKELADD", uppercasedName);
            
            var lowerCaseRule = rules.FirstOrDefault(rule => rule.Name == "LowerCaser");
            string lowerCasedName = RuleEval.GetValue(lowerCaseRule, entry);
            Assert.AreEqual("espen askeladd", lowerCasedName);
        }
    }
}
