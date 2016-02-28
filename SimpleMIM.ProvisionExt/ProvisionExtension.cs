using Microsoft.MetadirectoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.ProvisionExt
{
    public class ProvisionExtension : IMVSynchronization
    {
        public void Initialize()
        {
        }

        public void Provision(MVEntry mventry)
        {
            foreach (var provRule in Rules.ProvisionRules)
            {
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
                else
                {
                    //deprovision?
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
