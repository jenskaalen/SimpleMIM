using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IronPython.Runtime.Types;
using Newtonsoft.Json;
using SimpleMIM.Flow.Py;

namespace SimpleMIM.Flow.RuleLoading
{
    public class FileRuleLoader : IRuleLoader
    {
        private readonly List<string> _fileNames;

        public FileRuleLoader(string filename) : this(new List<string>(){ filename })
        {    
        }

        public FileRuleLoader(List<string> fileNames)
        {
            _fileNames = fileNames;
        }

        public List<FlowRule> LoadRules()
        {
            var flowRules = new List<FlowRule>();

            for (int i = 0; i < _fileNames.Count; i++)
            {
                string rulesText = File.ReadAllText(_fileNames[i]);
                List<FlowRule> flowRulesFromFile = JsonConvert.DeserializeObject<List<FlowRule>>(rulesText);
                flowRules.AddRange(flowRulesFromFile);
            }

            var scriptBuilder = new StringBuilder();

            foreach (FlowRule flowRule in flowRules.Where(rule => rule.ExpressionType == ExpressionType.Python))
            {
                string pyFunc = FuncCreator.GenerateFunction(flowRule.Name, "entry", flowRule.Expression);
                scriptBuilder.Append(pyFunc);
                scriptBuilder.Append(Environment.NewLine);
            }

            Core.RegisterFlowScript(scriptBuilder.ToString());
            return flowRules;
        }

        public List<FlowRule> LoadRules(List<string> ruleNames)
        {
            var flowRules = LoadRules();
            return flowRules.Where(rule => ruleNames.Any(ruleName => ruleName == rule.Name)).ToList();
        }
    }
}