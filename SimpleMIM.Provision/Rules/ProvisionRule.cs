namespace SimpleMIM.Provision.Rules
{
    public class ProvisionRule
    {
        public AttributeRule[] AttributeRules { get; set; }
        /// <summary>
        /// Names of Management Agents who are required for the mventry to have a connector to
        /// </summary>
        public string[] RequiredAgents { get; set; }
    }
}
