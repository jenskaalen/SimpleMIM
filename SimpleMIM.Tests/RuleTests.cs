using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMIM.Provision;
using SimpleMIM.Provision.Rules;
using SimpleMIM.Provision.Solvers;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class RuleTests
    {
        [TestMethod]
        public void SimpleRuleSolver_solves_AttributeRule()
        {
            var mventry = new MockMventry();
            mventry["uid"].Value = "espenaske";


            var attributeRule = new AttributeRule()
            {
                IsPresent = true,
                Attribute = "uid",
                RequiredValue = "espenaske"
            };

            var provRule = new ProvisionRule();
            provRule.AttributeRules = new[] { attributeRule };

            var ruleSolver = new SimpleRuleSolver(provRule);
            bool solved = ruleSolver.PassesRule(mventry);
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void SimpleRuleSolver_solves_AgentRequirement()
        {
            var mventry = new MockMventry();
            mventry.ConnectedMAs["HR MA"].Connectors.StartNewConnector("person");

            var provRule = new ProvisionRule();
            //provRule.AttributeRules = new[] { attributeRule };
            provRule.RequiredAgents = new [] { "HR MA" };

            var ruleSolver = new SimpleRuleSolver(provRule);
            bool solved = ruleSolver.PassesRule(mventry);
            Assert.IsTrue(solved);
        }
    }
}
