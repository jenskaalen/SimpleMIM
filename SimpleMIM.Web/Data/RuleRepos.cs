using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow.Data;

namespace SimpleMIM.Web.Data
{
    public class RuleRepos
    {
        private static MemoryProvRules _provRules = new MemoryProvRules();

        //public static SqlFlowRuleRepository FlowRules { get; } = new SqlFlowRuleRepository();
        public static SqlFlowRuleRepo FlowRules { get; } = new SqlFlowRuleRepo();

        public static MemoryProvRules ProvRules
        {
            get { return _provRules; }
        }
    }
}
