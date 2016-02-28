using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Py;

namespace SimpleMIM.ProvisionExt
{
    public class ProvisionEval
    {
        public static bool PassesCondition(ProvisionRule rule, MVEntry entry)
        {
            if (entry.ObjectType != rule.SourceObject)
                return false;

            if (rule.RuleType == RuleType.Python)
            {
                var func = Core.GetProvisionFunction(rule.Id);
                return func(entry);
            }

            throw new NotImplementedException();
        }

        public static void ApplyInitialFlows(ProvisionRule rule, CSEntry csentry, MVEntry mventry)
        {
            foreach (string initialFlow in rule.InitialFlows)
            {
                //TODO: handle non existant rule
                var flowRule = Rules.FlowRules.FirstOrDefault(flow => flow.Name == initialFlow);
                csentry[flowRule.Target].Value = RuleEval.GetValue(flowRule, mventry);
            }
        }
    }
}
