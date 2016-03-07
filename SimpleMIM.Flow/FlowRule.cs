using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        public string TargetAttribute { get; set; }
        public string Condition { get; set; }
        public string Expression { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ExpressionType RuleType { get; set; }
    }

    public enum ExpressionType
    {
        Python,
        Simple
    }
}
