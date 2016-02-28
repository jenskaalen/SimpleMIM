using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMIM.Flow;

namespace SimpleMIM.ProvisionExt.ProvisionRuleLoading
{
    public interface IRuleLoader
    {
        List<ProvisionRule> LoadRules();
    }
}
