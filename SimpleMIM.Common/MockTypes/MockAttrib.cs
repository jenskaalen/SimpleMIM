using System;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public class MockAttrib : Attrib
    {
        public MockAttrib(string name)
        {
            Name = name;
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override string Name { get; }
        public override AttributeType DataType { get; }
        //TODO: what about primitives?
        public override bool IsPresent { get { return Value != null || StringValue != null || ReferenceValue != null; } }

        public override bool IsMultivalued { get; }
        public override ValueCollection Values { get; set; }
        public override string Value { get; set; }
        public override string StringValue { get; set; }
        public override ReferenceValue ReferenceValue { get; set; }
        public override byte[] BinaryValue { get; set; }
        public override long IntegerValue { get; set; }
        public override bool BooleanValue { get; set; }
        public override DateTime LastContributionTime { get; }
        public override ManagementAgent LastContributingMA { get; }
    }
}