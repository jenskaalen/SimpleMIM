using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Flow.RuleLoading;

namespace SimpleMIM.Flow
{
    public class RulesExtension: IMASynchronization
    {
        private List<FlowRule> _flows;

        public void Initialize()
        {
            var ruleLoader = new FileRuleLoader(@"C:\Config\flowRules.json");
            _flows = ruleLoader.LoadRules();
        }

        public void Terminate()
        {
        }

        public bool FilterForDisconnection(CSEntry csentry)
        {
            throw new NotImplementedException();
        }

        public bool ResolveJoinSearch(string joinCriteriaName, CSEntry csentry, MVEntry[] rgmventry, out int imventry,
            ref string MVObjectType)
        {
            throw new NotImplementedException();
        }

        public bool ShouldProjectToMV(CSEntry csentry, out string MVObjectType)
        {
            throw new NotImplementedException();
        }

        public void MapAttributesForImport(string FlowRuleName, CSEntry csentry, MVEntry mventry)
        {
            FlowRule flowRule = _flows.FirstOrDefault(rule => rule.Name == FlowRuleName);

            if (flowRule == null)
                throw new Exception("Couldnt find flowrule " + FlowRuleName);

            dynamic val = RuleEval.GetValue(flowRule, mventry);

            if (val != null)
                mventry[flowRule.Target].Value = val;
        }

        public void MapAttributesForJoin(string FlowRuleName, CSEntry csentry, ref ValueCollection values)
        {
            throw new NotImplementedException();
        }

        public void MapAttributesForExport(string FlowRuleName, MVEntry mventry, CSEntry csentry)
        {
            FlowRule flowRule = _flows.FirstOrDefault(rule => rule.Name == FlowRuleName);

            if (flowRule == null)
                throw new Exception("Couldnt find flowrule " + FlowRuleName);

            dynamic val = RuleEval.GetValue(flowRule, mventry);

            if (val != null)
                csentry[flowRule.Target].Value = val;
        }

        public DeprovisionAction Deprovision(CSEntry csentry)
        {
            throw new NotImplementedException();
        }
    }
}
