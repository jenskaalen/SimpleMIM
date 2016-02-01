namespace SimpleMIM.Provision.Rules
{
    public class AttributeRule
    {
        public string Attribute { get; set; }
        public bool? IsPresent { get; set; }
        public string RequiredValue { get; set; }
    }
}