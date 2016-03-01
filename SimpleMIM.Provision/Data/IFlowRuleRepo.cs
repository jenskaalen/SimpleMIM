using System.Collections.Generic;
using SimpleMIM.Provision.Rules;

namespace SimpleMIM.Provision.Data
{
    interface IProvisionRuleRepo
    {
        List<ProvisionRule> GetAllRules();
        void SaveRule(ProvisionRule rule);
    }
}
