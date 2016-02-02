using System.Linq;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Provision.Rules;

namespace SimpleMIM.Provision.Solvers
{
    public class SimpleRuleSolver : IRuleSolver
    {
        public ProvisionRule Rule { get; }

        public SimpleRuleSolver(ProvisionRule rule)
        {
            Rule = rule;
        }
        
        public bool PassesRule(MVEntry mventry)
        {
            return PassesObjectRules(mventry) && CheckAgentRequirements(mventry) && PassesAttributeRules(mventry);
        }

        private bool PassesObjectRules(MVEntry mventry)
        {
            if (Rule.RequiredObjects == null || Rule.RequiredObjects.Length <= 0)
                return true;

            return Rule.RequiredObjects.Contains(mventry.ObjectType);
        }


        private bool MatchValue(Attrib attrib, string requiredValue)
        {
            if (attrib.Value == requiredValue)
                return true;

            return false;
        }

        private bool CheckAgentRequirements(MVEntry mventry)
        {
            if (Rule.RequiredAgents == null)
                return true;

            foreach (var ma in Rule.RequiredAgents)
            {
                if (mventry.ConnectedMAs[ma].Connectors.Count <= 0)
                    return false;
            }

            return true;
        }

        private bool PassesAttributeRules(MVEntry mventry)
        {
            if (Rule.AttributeRules == null)
                return true;

            foreach (var rule in Rule.AttributeRules)
            {
                if (!mventry[rule.Attribute].IsPresent)
                    return false;

                if (rule.IsPresent != null && rule.IsPresent != mventry[rule.Attribute].IsPresent)
                    return false;

                if (rule.RequiredValue != null)
                {
                    bool valueMatches = MatchValue(mventry[rule.Attribute], rule.RequiredValue);

                    if (!valueMatches)
                        return false;
                }
            }

            return true;
        }
    }
}