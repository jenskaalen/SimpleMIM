using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.Flow.RuleLoading
{
    interface IRuleLoader
    {
        List<FlowRule> LoadRules();
        List<FlowRule> LoadRules(List<string> ruleNames);
    }
}
