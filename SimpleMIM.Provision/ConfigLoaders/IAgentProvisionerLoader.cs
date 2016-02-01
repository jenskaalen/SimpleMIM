using System.IO;

namespace SimpleMIM.Provision.ConfigLoaders
{
    public interface IAgentProvisionerLoader
    {
        AgentProvisioner[] LoadAgentProvisioners(FileInfo configFile);
        AgentProvisioner[] LoadAgentProvisioners(string fileData);
    }
}
