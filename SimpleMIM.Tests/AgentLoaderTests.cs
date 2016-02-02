using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMIM.Provision;
using SimpleMIM.Provision.ConfigLoaders;

namespace MIMSimplifier.Tests
{
    [TestClass]
    public class AgentLoaderTests
    {
        [TestMethod]
        public void Loader_AgentProvisioner_load_from_json()
        {
            string json = File.ReadAllText("Samples\\agentProvisioners.json");

            IAgentProvisionerLoader loader = new AgentProvisionerLoader();
            AgentProvisioner[] provisioners = loader.LoadAgentProvisioners(json);

            Assert.AreEqual(2, provisioners.Length);
            Assert.IsTrue(provisioners[1].AdvancedAttributeSetters.Length >= 1);
        }
    }
}
