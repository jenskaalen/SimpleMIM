namespace SimpleMIM.Provision.Rules
{
    public class ProvisionRule
    {
        /// <summary>
        /// The object types required
        /// </summary>
        public string[] RequiredObjects { get; set; }
        public AttributeRule[] AttributeRules { get; set; }
        /// <summary>
        /// Names of Management Agents who are required for the mventry to have a connector to
        /// </summary>
        public string[] RequiredAgents { get; set; }
    }
}
