using Microsoft.MetadirectoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;
using SimpleMIM.ProvisionExt.Data;
using SimpleMIM.PythonConfiguration.Py;

namespace SimpleMIM.ProvisionExt
{
    public class ProvisionExtension : IMVSynchronization
    {
        private List<ProvisionRule> _provRules;

        public void Initialize()
        {
            var flowRuleRepo = new SqlFlowRuleRepo();
            var provRuleRepo = new SqlProvRuleRepo();
            _provRules = provRuleRepo.GetAllRules();

            var scriptBuilder = new StringBuilder();

            foreach (ProvisionRule provRule in _provRules.Where(rule => rule.RuleType == RuleType.Python))
            {
                string pyFunc = FuncCreator.GenerateFunction(provRule.Name, "entry", provRule.Condition);
                scriptBuilder.Append(pyFunc);
                scriptBuilder.Append(Environment.NewLine);
            }

            Core.RegisterProvisionScript(scriptBuilder.ToString());


            var flowRules = flowRuleRepo.GetAllRules();
            var flowBuilder = new StringBuilder();

            foreach (FlowRule flowRule in flowRules.Where(rule => rule.RuleType == ExpressionType.Python))
            {
                string pyFunc = FuncCreator.GenerateFunction(flowRule.Name, "source, target", flowRule.Expression);
                flowBuilder.Append(pyFunc);
                flowBuilder.Append(Environment.NewLine);
            }

            Core.RegisterFlowScript(flowBuilder.ToString());
        }

        public void Provision(MVEntry mventry)
        {
            foreach (var provRule in _provRules)
            {
                if (provRule.SourceObject != mventry.ObjectType)
                    continue;

                bool passes = ProvisionEval.PassesCondition(provRule, mventry);

                if (passes)
                {
                    if (mventry.ConnectedMAs[provRule.Agent].Connectors.Count < 1)
                    {
                        //Provision
                        CSEntry csentry =
                            mventry.ConnectedMAs[provRule.Agent].Connectors.StartNewConnector(provRule.TargetObject);

                        ProvisionEval.ApplyInitialFlows(provRule, csentry, mventry);
                        csentry.CommitNewConnector();
                    }
                }
                else if (provRule.Deprovision)
                {
                    mventry.ConnectedMAs[provRule.Agent].Connectors.DeprovisionAll();
                }
            }
        }

        public bool ShouldDeleteFromMV(CSEntry csentry, MVEntry mventry)
        {
            return false;
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
