using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using Newtonsoft.Json;
using SimpleMIM.Provision.ConfigLoaders;
using SimpleMIM.Provision.Rules;
using SimpleMIM.Provision.Solvers;

namespace SimpleMIM.Provision
{
    public class SimpleProvision: IMVSynchronization
    {
        private AgentProvisioner[] _agentProvisioners;
        private string _configLocation;

        private enum ProvisionState
        {
            Provision,
            Deprovision,
            Unchanged
        }

        public void Initialize()
        {
            _configLocation = @"C:\config\provisionRules.json";
            IAgentProvisionerLoader provisionLoader = new AgentProvisionerLoader();
            _agentProvisioners = provisionLoader.LoadAgentProvisioners(new FileInfo(_configLocation));
        }
        public void Terminate()
        {
            //throw new NotImplementedException();
        }

        public void Provision(MVEntry mventry)
        {
            foreach (AgentProvisioner provisioner in _agentProvisioners)
            {
                if (!provisioner.HandledObjectTypes.Contains(mventry.ObjectType))
                    continue;

                bool passesCriteria = provisioner.PassesProvisionCriteria(mventry);
                ProvisionState state = GetProvisionState(mventry, provisioner.MAName, passesCriteria);

                if (state == ProvisionState.Provision)
                    provisioner.Provision(mventry);
                else if (state == ProvisionState.Deprovision)
                    provisioner.Deprovision(mventry);
            }
        }

        private ProvisionState GetProvisionState(MVEntry mventry, string maName, bool passesCriteria)
        {
            if (passesCriteria)
            {
                if (mventry.ConnectedMAs[maName].Connectors.Count > 0)
                    return ProvisionState.Unchanged;

                return ProvisionState.Provision;
            }

            if (mventry.ConnectedMAs[maName].Connectors.Count > 0)
                return ProvisionState.Deprovision;
                
            return ProvisionState.Unchanged;
        }

        public bool ShouldDeleteFromMV(CSEntry csentry, MVEntry mventry)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
