using System.IO;
using Newtonsoft.Json;

namespace SimpleMIM.Provision.ConfigLoaders
{
    public class AgentProvisionerLoader : IAgentProvisionerLoader
    {
        public AgentProvisioner[] LoadAgentProvisioners(FileInfo configFile)
        {
            string jsonData = File.ReadAllText(configFile.FullName);

            var provisioners = JsonConvert.DeserializeObject<AgentProvisioner[]>(jsonData);
            return provisioners;
        }

        public AgentProvisioner[] LoadAgentProvisioners(string fileData)
        {
            var provisioners = JsonConvert.DeserializeObject<AgentProvisioner[]>(fileData);
            return provisioners;
        }
    }
}