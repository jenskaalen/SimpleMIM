using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow;
using SimpleMIM.Flow.RuleLoading;
using SimpleMIM.ProvisionExt.ProvisionRuleLoading;

namespace SimpleMIM.ProvisionExt
{
    public class Rules
    {
        private static List<FlowRule> _flowRules;
        private static List<ProvisionRule> _provRules;

        public static List<FlowRule> FlowRules
        {
            get
            {
                if (_flowRules == null)
                { 
                    var fileRuleLoader = new FileRuleLoader(@"C:\Config\flowRules.json");
                    _flowRules = fileRuleLoader.LoadRules();
                }

                return _flowRules;
            }
        }

        public static List<ProvisionRule> ProvisionRules
        {
            get
            {
                if (_provRules == null)
                {
                    var fileProvRuleLoader = new FileProvRuleLoader(@"C:\Config\provRules.json");
                    _provRules = fileProvRuleLoader.LoadRules();
                }

                return _provRules;
            }
        }

        public static void SetRules(List<ProvisionRule> provRules, List<FlowRule> flowRules)
        {
            _provRules = provRules;
            _flowRules = flowRules;
        }
    }
}
