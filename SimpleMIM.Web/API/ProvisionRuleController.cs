using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleMIM.Common.MockTypes;
using SimpleMIM.ProvisionExt;
using SimpleMIM.PythonConfiguration.Py;
using SimpleMIM.Web.Data;
using SimpleMIM.Web.Models;

namespace SimpleMIM.Web.API
{
    public class ProvisionRuleController : ApiController
    {
        [HttpGet]
        public List<ProvisionRule> GetAll()
        {
            return RuleRepos.ProvRules.GetAllRules();
        }

        [HttpPost]
        public void Save(ProvisionRule provisionRule)
        {
            RuleRepos.ProvRules.SaveRule(provisionRule);
        }

        [HttpPost]
        public object Test(ProvRuleTest test)
        {
            test.ProvisionRule.Condition = test.ProvisionRule.Condition;
            string dummyId = "testId" + Guid.NewGuid().ToString().Replace("-", "");
            var func = FuncCreator.GenerateFunction(dummyId, "entry", test.ProvisionRule.Condition);
            Core.RegisterFlowScript(func);


            var mventryMock = new MockMventry("person");

            foreach (var attrib in test.Attributes)
            {
                mventryMock[attrib.Name].Value = attrib.Value;
            }

            var pyFunc = Core.GetFlowFunction(dummyId);
            return pyFunc(mventryMock);
        }
    }
}
