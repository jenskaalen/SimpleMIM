using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;
using SimpleMIM.PythonConfiguration.Py;

namespace SimpleMIM.Flow
{
    public class RuleEval
    {
        public static dynamic GetValue(FlowRule rule, MVEntry entry)
        {
            if (rule.ExpressionType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                return func(entry);
            }

            throw new NotImplementedException();
        }

        public static dynamic GetValue(FlowRule rule, CSEntry entry)
        {
            if (rule.ExpressionType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                return func(entry);
            }

            throw new NotImplementedException();
        }
    }
}
