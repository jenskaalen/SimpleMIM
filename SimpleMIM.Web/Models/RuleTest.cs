using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.Common.MockTypes;
using SimpleMIM.Flow;

namespace SimpleMIM.Web.Models
{
    public class RuleTest
    {
        public FlowRule FlowRule { get; set; }
        public IMockEntry Source { get; set; }
        public IMockEntry Target { get; set; }
    }
}
