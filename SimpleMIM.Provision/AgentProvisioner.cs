using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Provision.Rules;
using SimpleMIM.Provision.Solvers;

namespace SimpleMIM.Provision
{
    public class AgentProvisioner
    {
        private IRuleSolver _ruleSolver;

        public AgentProvisioner(string maName, string provisionedObjectType,  ProvisionRule provisionRule, SimpleAttributeSetter[] simpleAttributeSetters, AdvancedAttributeSetter[] advancedAttributeSetters, string[] handledObjectTypes)
        {
            MAName = maName;
            ProvisionedObjectType = provisionedObjectType;
            ProvisionRule = provisionRule;
            _ruleSolver = new SimpleRuleSolver(ProvisionRule);
            SimpleAttributeSetters = simpleAttributeSetters;
            AdvancedAttributeSetters = advancedAttributeSetters;
            HandledObjectTypes = handledObjectTypes;

            if (simpleAttributeSetters == null)
                SimpleAttributeSetters = new SimpleAttributeSetter[0];

            if (advancedAttributeSetters == null)
                AdvancedAttributeSetters = new AdvancedAttributeSetter[0];

            if (HandledObjectTypes == null)
                HandledObjectTypes = new string[0];
        }

        public string MAName { get; }
        //TODO can maybe be private
        public ProvisionRule ProvisionRule { get; }
        public string ProvisionedObjectType { get; }
        public SimpleAttributeSetter[] SimpleAttributeSetters { get; }
        public AdvancedAttributeSetter[] AdvancedAttributeSetters { get; }
        public string[] HandledObjectTypes { get; set; }

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

            foreach (var provisionSetter in SimpleAttributeSetters)
            {
                csentry[provisionSetter.CSAttribute].Value = mventry[provisionSetter.MVAttribute].Value;
            }

            foreach (AdvancedAttributeSetter attributeSetter in AdvancedAttributeSetters)
            {
                string value = AttributeFormatter.FormatValue(mventry, attributeSetter.ReplaceFormat);
                if (attributeSetter.CSAttribute.ToLower() == "dn")
                    csentry.DN = mventry.ConnectedMAs[MAName].CreateDN(value);
                else 
                    csentry[attributeSetter.CSAttribute].Value = value;
            }

            csentry.CommitNewConnector();
        }
    }
}
