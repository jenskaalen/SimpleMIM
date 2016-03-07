using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Flow;

namespace SimpleMIM.ProvisionExt
{
    public class ProvisionRule
    {
        public string Name { get; set; }
        public string SourceObject { get; set; }
        public string TargetObject { get; set; }
        public string Agent { get; set; }
        public string Condition { get; set; }
        public RuleType RuleType { get; set; }
        public FlowRule[] InitialFlows { get; set; }
        public bool Deprovision { get; set; }
    }
}
