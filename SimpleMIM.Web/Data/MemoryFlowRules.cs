using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;

namespace SimpleMIM.Web.Data
{
    public class MemoryFlowRules: IFlowRuleRepo
    {
        List<FlowRule> _flowRules
        {
            get
            {
                ObjectCache cache = MemoryCache.Default;

                if (cache["flowRules"] == null)
                    cache["flowRules"] = new List<FlowRule>();

                return (List<FlowRule>) cache["flowRules"];
            }
        } 

        public List<FlowRule> GetAllRules()
        {
            return _flowRules;
        }

        public void SaveRule(FlowRule rule)
        {
            var existingRule = _flowRules.FirstOrDefault(flowRule => flowRule.Name == rule.Name);

            if (existingRule != null)
            {
                _flowRules.Remove(existingRule);
            }

            _flowRules.Add(rule);
        }
    }
}
