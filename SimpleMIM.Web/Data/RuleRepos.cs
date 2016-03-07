using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow.Data;
using SimpleMIM.ProvisionExt.Data;

namespace SimpleMIM.Web.Data
{
    public class RuleRepos
    {
        //public static SqlFlowRuleRepository FlowRules { get; } = new SqlFlowRuleRepository();
        public static SqlFlowRuleRepo FlowRules { get; } = new SqlFlowRuleRepo();

        public static SqlProvRuleRepo ProvRules { get; } = new SqlProvRuleRepo();
    }
}
