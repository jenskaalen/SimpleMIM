using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using SimpleMIM.ProvisionExt;
using SimpleMIM.ProvisionExt.Data;

namespace SimpleMIM.Web.Data
{
    public class MemoryProvRules : IProvisionRuleRepo
    {
        List<ProvisionRule> _provRules
        {
            get
            {
                ObjectCache cache = MemoryCache.Default;

                if (cache["provRules"] == null)
                    cache["provRules"] = new List<ProvisionRule>();

                return (List<ProvisionRule>)cache["provRules"];
            }
        }

        public List<ProvisionRule> GetAllRules()
        {
            return _provRules;
        }

        public void SaveRule(ProvisionRule rule)
        {
            var existingRule = _provRules.FirstOrDefault(provRule => provRule.Name == rule.Name);

            if (existingRule != null)
            {
                _provRules.Remove(existingRule);
            }

            _provRules.Add(rule);
        }
    }
}