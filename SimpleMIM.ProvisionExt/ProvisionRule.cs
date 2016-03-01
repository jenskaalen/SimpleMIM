using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.ProvisionExt
{
    public class ProvisionRule
    {
        public string Id { get; set; }
        public string SourceObject { get; set; }
        public string TargetObject { get; set; }
        public string Agent { get; set; }
        public string Condition { get; set; }
        public RuleType RuleType { get; set; }
        public string[] InitialFlows { get; set; }
        public bool Deprovision { get; set; }
    }
}
