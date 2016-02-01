using System;
using Microsoft.MetadirectoryServices;
using SimpleMIM.Provision.Solvers;

namespace SimpleMIM.Provision.Rules
{
    class MimRuleSolver : IRuleSolver
    {
        public ProvisionRule Rule { get; }

        public bool PassesRule(MVEntry mventry)
        {
            throw new NotImplementedException();
        }
    }
}