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
        public MockMventry Source { get; set; }
        public MockMventry Target { get; set; }
    }
}
