using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;
using SimpleMIM.ProvisionExt.Data;

namespace SimpleMIM.ProvisionExt
{
    public class RuleFactory
    {
        private static List<FlowRule> _flowRules;
        private static List<ProvisionRule> _provRules;
        

        public static List<FlowRule> FlowRules
        {
            get
            {
                if (_flowRules == null)
                { 
                    var fileRuleLoader = new FileFlowRuleRepo(@"C:\Config\flowRules.json");
                    _flowRules = fileRuleLoader.GetAllRules();
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
                    var fileProvRuleLoader = new FileProvRuleRepo(@"C:\Config\provRules.json");
                    _provRules = fileProvRuleLoader.GetAllRules();
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
