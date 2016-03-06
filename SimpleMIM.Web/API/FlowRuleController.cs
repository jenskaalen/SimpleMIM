using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleMIM.Common.MockTypes;
using SimpleMIM.Flow;
using SimpleMIM.PythonConfiguration.Py;
using SimpleMIM.Web.Data;
using SimpleMIM.Web.Models;

namespace SimpleMIM.Web.API
{
    public class FlowRuleController : ApiController
    {
        [HttpGet]
        public List<FlowRule> GetAll()
        {
            return RuleRepos.FlowRules.GetAllRules();
        }

        [HttpPost]
        public void Save(FlowRule flowRule)
        {
            RuleRepos.FlowRules.SaveRule(flowRule);
        }

        [HttpPost]
        public object Test(RuleTest test)
        {
            test.FlowRule.Expression = test.FlowRule.Expression.TrimEnd('"').TrimStart('"');
            string dummyId = "testId" + Guid.NewGuid().ToString().Replace("-", "");
            var func = FuncCreator.GenerateFunction(dummyId, "source, target", test.FlowRule.Expression);
            Core.RegisterFlowScript(func);

            var pyFunc = Core.GetFlowFunction(dummyId);
            pyFunc(test.Source, test.Target);
            return test.Target;
        }
    }
}
