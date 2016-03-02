using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.Web.Data
{
    public class RuleRepos
    {
        private static MemoryProvRules _provRules = new MemoryProvRules();

        public static SqlFlowRuleRepository FlowRules { get; } = new SqlFlowRuleRepository();

        public static MemoryProvRules ProvRules
        {
            get { return _provRules; }
        }
    }
}
