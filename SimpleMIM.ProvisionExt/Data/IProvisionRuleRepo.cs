using System.Collections.Generic;

namespace SimpleMIM.ProvisionExt.Data
{
    public interface IProvisionRuleRepo
    {
        List<ProvisionRule> GetAllRules();
        void SaveRule(ProvisionRule rule);
    }
}
