using System.Collections.Generic;
using SimpleMIM.ProvisionExt;

namespace SimpleMIM.Web.Models
{
    public class ProvRuleTest
    {
        public ProvisionRule ProvisionRule { get; set; }
        public List<MIMAttribute> Attributes { get; set; }
    }
}