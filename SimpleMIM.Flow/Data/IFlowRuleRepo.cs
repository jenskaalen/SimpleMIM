using System.Collections.Generic;

namespace SimpleMIM.Flow.Data
{
    interface IFlowRuleRepo
    {
        List<FlowRule> GetAllRules();
        void SaveRule(FlowRule rule);
    }
}
