using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMIM.Flow
{
    public class FlowRule
    {
        public string Name { get; set; }
        public string Target { get; set; }
        public string Condition { get; set; }
        public string Expression { get; set; }
        public ExpressionType ExpressionType { get; set; }
    }

    public enum ExpressionType
    {
        Python,
        Simple
    }
}
