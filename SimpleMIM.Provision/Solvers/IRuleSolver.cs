using Microsoft.MetadirectoryServices;
using SimpleMIM.Provision.Rules;

namespace SimpleMIM.Provision.Solvers
{
    interface IRuleSolver
    {
        ProvisionRule Rule { get; }
        bool PassesRule(MVEntry mventry);
    }
}
