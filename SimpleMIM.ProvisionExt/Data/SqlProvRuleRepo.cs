using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;

namespace SimpleMIM.ProvisionExt.Data
{
    public class SqlProvRuleRepo : IProvisionRuleRepo
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["SimpleMIM"].ConnectionString;

        public List<ProvisionRule> GetAllRules()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var provisionRules = conn.Query<ProvisionRule>("select * from ProvisionRule").ToList();

                foreach (var provisionRule in provisionRules)
                {
                    provisionRule.InitialFlows = conn.Query<FlowRule>(
                        @"select FlowRule.* from ProvisionRule ProvRule
inner join InitialFlowRule InitFlow on ProvRule.Name = InitFlow.ProvisionRuleName 
inner join FlowRule FlowRule on InitFlow.FlowRuleName = FlowRule.Name
where ProvRule.Name = @RuleName", new { RuleName = provisionRule.Name }).ToArray();
                }

                return provisionRules;
            }
        }

        public void SaveRule(ProvisionRule rule)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var existingRule = conn.Query<ProvisionRule>("select * from ProvisionRule where Name = @Name", new { Name = rule.Name }).FirstOrDefault();

                if (existingRule == null)
                {
                    conn.Execute(
                        "insert into ProvisionRule (Name, SourceObject, TargetObject, Agent, Condition, RuleType) values (@Name, @SourceObject, @TargetObject, @Agent, @Condition, @RuleType)",
                        rule);
                }
                else
                {
                    conn.Execute(
                        "update ProvisionRule " +
                        "set SourceObject = @SourceObject, TargetObject = @TargetObject, Agent = @Agent, Condition = @Condition " +
                        "where Name = @Name",
                        rule);
                }

                conn.Execute("delete from InitialFlowRule where ProvisionRuleName = @ProvisionRuleName",
                    new {ProvisionRuleName =  rule.Name});

                if (rule.InitialFlows != null)
                {
                    foreach (FlowRule initialFlow in rule.InitialFlows)
                    {
                        conn.Execute("insert into InitialFlowRule (ProvisionRuleName, FlowRuleName) values (@ProvisionRuleName, @FlowRuleName))"
                            , new { ProvisionRuleName = rule.Name, FlowRuleName = initialFlow.Name });
                    }
                }
            }
        }
    }
}