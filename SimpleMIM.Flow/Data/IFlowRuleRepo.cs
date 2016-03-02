using System.Collections.Generic;

namespace SimpleMIM.Flow.Data
{
    public interface IFlowRuleRepo
    {
        List<FlowRule> GetAllRules();
        void SaveRule(FlowRule rule);
    }
}
