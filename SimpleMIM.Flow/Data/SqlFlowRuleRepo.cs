using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace SimpleMIM.Flow.Data
{
    public class SqlFlowRuleRepo : IFlowRuleRepo
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["SimpleMIM"].ConnectionString;

        public List<FlowRule> GetAllRules()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<FlowRule>("select * from FlowRule").ToList();
            }
        }

        public void SaveRule(FlowRule rule)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var existingRule = conn.Query<FlowRule>("select * from FlowRule where Name = @Name", new { Name = rule.Name }).FirstOrDefault();

                if (existingRule == null)
                {
                    conn.Execute(
                        "insert into FlowRule (Name, TargetAttribute, Condition, Expression, RuleType) values (@Name, @TargetAttribute, @Condition, @Expression, @RuleType)",
                        rule);
                }
                else
                {
                    conn.Execute(
                        "update FlowRule " +
                        "set TargetAttribute = @TargetAttribute, Expression = @Expression  " +
                        "where Name = @Name",
                        rule);
                }
            }
        }
    }
}