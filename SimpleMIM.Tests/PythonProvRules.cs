﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MetadirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIMSimplifier.Tests.MockTypes;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;
using SimpleMIM.ProvisionExt;
using SimpleMIM.ProvisionExt.Data;
using SimpleMIM.PythonConfiguration.Py;
using RuleType = SimpleMIM.ProvisionExt.RuleType;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class PythonProvRules
    {
        [TestMethod]
        public void ProvRules_python_rules_are_registered()
        {

        }

        [TestMethod]
        public void ProvRules_python_rules_generate_condition_funcs()
        {
            var provRule = new SimpleMIM.ProvisionExt.ProvisionRule()
            {
                Name = "nana",
                RuleType = RuleType.Python,
                Agent = "TestAgent",
                Condition = "entry['FirstName'].Value == per",
                SourceObject = "person",
                TargetObject = "HRMUser"
            };

            string script = FuncCreator.GenerateFunction(provRule.Name, "entry", provRule.Condition);

            Core.RegisterProvisionScript(script);
            var func = Core.GetProvisionFunction(provRule.Name);
            Assert.IsNotNull(func);
        }

        [TestMethod]
        public void ProvRules_python_rules_are_loaded_from_file()
        {
            MVEntry entry = new MockMventry("person");
            entry["FirstName"].Value = "per";
            entry["LastName"].Value = "Askeladd";

            var loader = new FileProvRuleRepo("Samples\\pyProvRules.json");
            List<ProvisionRule> rules = loader.GetAllRules();

            var provRule = rules.FirstOrDefault();
            Assert.IsTrue(provRule.Agent == "TravelPortal MA");

            Assert.IsTrue(ProvisionEval.PassesCondition(provRule, entry));
        }

        [TestMethod]
        public void ProvRules_handles_condition()
        {
            var provRule = new SimpleMIM.ProvisionExt.ProvisionRule()
            {
                Name = "nunu",
                RuleType = RuleType.Python,
                Agent = "TestAgent",
                Condition = "entry['FirstName'].Value == 'per'",
                SourceObject = "person",
                TargetObject = "HRMUser",
                InitialFlows = new[] { new FlowRule() { Name = "UpperCaser" } }
            };

            var flowRules = new FileFlowRuleRepo("Samples\\pyFlowRules.json").GetAllRules();
            RuleFactory.SetRules(new List<ProvisionRule>() { provRule }, flowRules);

            string script = FuncCreator.GenerateFunction(provRule.Name, "entry", provRule.Condition);

            Core.RegisterProvisionScript(script);
            var func = Core.GetProvisionFunction(provRule.Name);
            Assert.IsNotNull(func);

            var mventry = new MockMventry("person");
            mventry["FirstName"].Value = "per";

            Assert.IsTrue(ProvisionEval.PassesCondition(provRule, mventry));

            var mventryInvalid = new MockMventry("person");
            mventryInvalid["FirstName"].Value = "sitrus";

            Assert.IsFalse(ProvisionEval.PassesCondition(provRule, mventryInvalid));

        }

        [TestMethod]
        public void ProvRules_initialflows_are_set()
        {
            var provRule = new SimpleMIM.ProvisionExt.ProvisionRule()
            {
                Name = "nunu",
                RuleType = RuleType.Python,
                Agent = "TestAgent",
                Condition = "entry['FirstName'].Value == per",
                SourceObject = "person",
                TargetObject = "HRMUser",
                InitialFlows = new [] { new FlowRule() {  Name = "UpperCaser" }  }
            };

            var flowRules = new FileFlowRuleRepo("Samples\\pyFlowRules.json").GetAllRules();
            RuleFactory.SetRules(new List<ProvisionRule>() { provRule }, flowRules);

            string script = FuncCreator.GenerateFunction(provRule.Name, "entry", provRule.Condition);

            Core.RegisterProvisionScript(script);
            var func = Core.GetProvisionFunction(provRule.Name);
            Assert.IsNotNull(func);

            var mventry = new MockMventry();
            mventry["FirstName"].Value = "Espen";
            mventry["LastName"].Value = "Askeladd";

            var csentry = new MockCsentry();
            ProvisionEval.ApplyInitialFlows(provRule, csentry, mventry);

            Assert.AreEqual("ESPEN ASKELADD", csentry["DisplayName"].Value);
        }
    }
}
