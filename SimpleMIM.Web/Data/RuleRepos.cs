using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.Web.Data
{
    public class RuleRepos
    {
        private static MemoryFlowRules _flowRules = new MemoryFlowRules();

        public static MemoryFlowRules FlowRules
        {
            get { return _flowRules; }
        }
    }
}
