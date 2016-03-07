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
        public static void Execute(FlowRule rule, CSEntry source, MVEntry target)
        {
            if (rule.RuleType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                func(source, target);
                return;
            }

            throw new NotImplementedException();
        }

        public static void Execute(FlowRule rule,MVEntry source, CSEntry target)
        {
            if (rule.RuleType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                func(source, target);
                return;
            }

            throw new NotImplementedException();
        }

        //public static void Execute(FlowRule rule, CSEntry csentry, MVEntry mventry)
        //{

        //}

        public static dynamic GetValue(FlowRule rule, MVEntry entry)
        {
            if (rule.RuleType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                return func(entry);
            }

            throw new NotImplementedException();
        }

        public static dynamic GetValue(FlowRule rule, CSEntry entry)
        {
            if (rule.RuleType == ExpressionType.Python)
            {
                var func = Core.GetFlowFunction(rule.Name);
                return func(entry);
            }

            throw new NotImplementedException();
        }
    }
}
