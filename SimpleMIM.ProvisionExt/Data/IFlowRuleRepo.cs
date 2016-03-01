using System.Collections.Generic;

namespace SimpleMIM.ProvisionExt.Data
{
    interface IProvisionRuleRepo
    {
        List<ProvisionRule> GetAllRules();
        void SaveRule(ProvisionRule rule);
    }
}
