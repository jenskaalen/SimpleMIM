using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Flow;
using SimpleMIM.PythonConfiguration.Py;

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
                var func = Core.GetProvisionFunction(rule.Name);
                return func(entry);
            }

            throw new NotImplementedException();
        }

        public static void ApplyInitialFlows(ProvisionRule rule, CSEntry csentry, MVEntry mventry)
        {
            if (rule.InitialFlows == null)
                return;

            foreach (var initialFlow in rule.InitialFlows)
            {
                RuleEval.Execute(initialFlow, mventry, csentry);
            }
        }
    }
}
