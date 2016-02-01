using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Provision.Rules;
using SimpleMIM.Provision.Solvers;

namespace SimpleMIM.Provision
{
    public class AgentProvisioner
    {
        private IRuleSolver _ruleSolver;

        public AgentProvisioner(string maName, string provisionedObjectType,  ProvisionRule provisionRule, ProvisionSetter[] provisionSetters)
        {
            MAName = maName;
            ProvisionedObjectType = provisionedObjectType;
            ProvisionRule = provisionRule;
            _ruleSolver = new SimpleRuleSolver(ProvisionRule);
            ProvisionSetters = provisionSetters;

            if (provisionSetters == null)
                ProvisionSetters = new ProvisionSetter[0];
        }

        public string MAName { get; }
        //TODO can maybe be private
        public ProvisionRule ProvisionRule { get; }
        public string ProvisionedObjectType { get; }
        public ProvisionSetter[] ProvisionSetters { get; }

        public bool PassesProvisionCriteria(MVEntry mventry)
        {
            return _ruleSolver.PassesRule(mventry);
        }

        public void Deprovision(MVEntry mventry)
        {
            mventry.ConnectedMAs[MAName].Connectors.DeprovisionAll();
        }

        public void Provision(MVEntry mventry)
        {
            CSEntry csentry = mventry.ConnectedMAs[MAName].Connectors.StartNewConnector(
                ProvisionedObjectType);

            foreach (var provisionSetter in ProvisionSetters)
            {
                csentry[provisionSetter.CSAttribute].Value = mventry[provisionSetter.MVAttribute].Value;
            }

            csentry.CommitNewConnector();
        }
    }

    public class ProvisionSetter
    {
        public string MVAttribute { get; set; }
        public string CSAttribute { get; set; }
    }
}
