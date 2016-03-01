using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SimpleMIM.PythonConfiguration.Py;

namespace SimpleMIM.ProvisionExt.Data
{
    public class FileProvRuleRepo : IProvisionRuleRepo
    {
        private readonly string _filename;

        public FileProvRuleRepo(string filename)
        {
            _filename = filename;
        }

        public List<ProvisionRule> GetAllRules()
        {
            var flowRules = new List<ProvisionRule>();

            string rulesText = File.ReadAllText(_filename);
            List<ProvisionRule> flowRulesFromFile = JsonConvert.DeserializeObject<List<ProvisionRule>>(rulesText);
            flowRules.AddRange(flowRulesFromFile);

            var scriptBuilder = new StringBuilder();

            foreach (ProvisionRule flowRule in flowRules.Where(rule => rule.RuleType == RuleType.Python))
            {
                string pyFunc = FuncCreator.GenerateFunction(flowRule.Id, "entry", flowRule.Condition);
                scriptBuilder.Append(pyFunc);
                scriptBuilder.Append(Environment.NewLine);
            }

            Core.RegisterProvisionScript(scriptBuilder.ToString());

            return flowRules;
        }

        public void SaveRule(ProvisionRule rule)
        {
            throw new NotImplementedException();
        }
    }
}