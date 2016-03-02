using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SimpleMIM.PythonConfiguration.Py;

namespace SimpleMIM.Flow.Data
{
    public class FileFlowRuleRepo : IFlowRuleRepo
    {
        private readonly List<string> _fileNames;

        public FileFlowRuleRepo(string filename) : this(new List<string>(){ filename })
        {    
        }

        public FileFlowRuleRepo(List<string> fileNames)
        {
            _fileNames = fileNames;
        }

        public List<FlowRule> GetAllRules()
        {
            var flowRules = new List<FlowRule>();

            for (int i = 0; i < _fileNames.Count; i++)
            {
                string rulesText = File.ReadAllText(_fileNames[i]);
                List<FlowRule> flowRulesFromFile = JsonConvert.DeserializeObject<List<FlowRule>>(rulesText);
                flowRules.AddRange(flowRulesFromFile);
            }

            var scriptBuilder = new StringBuilder();

            foreach (FlowRule flowRule in flowRules.Where(rule => rule.RuleType == ExpressionType.Python))
            {
                string pyFunc = FuncCreator.GenerateFunction(flowRule.Name, "entry", flowRule.Expression);
                scriptBuilder.Append(pyFunc);
                scriptBuilder.Append(Environment.NewLine);
            }

            Core.RegisterFlowScript(scriptBuilder.ToString());
            return flowRules;
        }

        public void SaveRule(FlowRule rule)
        {
            throw new NotImplementedException();
        }
    }
}