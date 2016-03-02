
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using Dapper;
using SimpleMIM.Flow;
using SimpleMIM.Flow.Data;
using SimpleMIM.ProvisionExt;
using SimpleMIM.ProvisionExt.Data;

namespace SimpleMIM.Web.Data
{
    public class ConfigHelper
    {
        public static string ConnectionString => WebConfigurationManager.ConnectionStrings["SimpleMIM"].ConnectionString;
    }

    public class SqlFlowRuleRepository: IFlowRuleRepo
    {

        public List<FlowRule> GetAllRules()
        {
            using (var conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                return conn.Query<FlowRule>("select * from FlowRule").ToList();
            }
        }

        public void SaveRule(FlowRule rule)
        {
            using (var conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                var existingRule = conn.Query<FlowRule>("select * from FlowRule where Name = @Name", new { Name = rule.Name }).FirstOrDefault();

                if (existingRule == null)
                {
                    conn.Execute(
                        "insert into FlowRule (Name, TargetAttribute, Condition, Expression, RuleType) values (@Name, @TargetAttribute, @Condition, @Expression, @RuleType)",
                        rule);
                }
            }
        }
    }

    public class SqlProvisionRuleRepository : IProvisionRuleRepo
    {
        public List<ProvisionRule> GetAllRules()
        {
            using (var conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                //TODO: need to handle InitialFlows
                return conn.Query<ProvisionRule>("select * from ProvisionRule").ToList();
            }
        }

        public void SaveRule(ProvisionRule rule)
        {
            using (var conn = new SqlConnection(ConfigHelper.ConnectionString))
            {
                var existingRule = conn.Query<FlowRule>("select * from ProvisionRule where Name = @Name", new { Name = rule.Name }).FirstOrDefault();
                
                if (existingRule == null)
                {
                    conn.Execute(
                        "insert into ProvisionRule ([Name],[SourceObject],[TargetObject],[Agent],[Condition],[Deprovision],[InitialFlows],[RuleType]) values (@Name, @SourceObject, @TargetObject, @Agent, @Condition, @Deprovision, @InitialFlows, @RuleType)"
                        ,
                        new { rule.Name, rule.SourceObject, rule.TargetObject, rule.Agent, rule.Condition, rule.Deprovision, InitialFlows = rule.InitialFlows == null ? null : String.Join(";", rule.InitialFlows), rule.RuleType });
                }
            }
        }
    }
}