using System;
using System.Collections.Generic;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public class MockCsentry: CSEntry
    {
        private readonly Dictionary<string, MockAttrib> _attributes;

        public MockCsentry()
        {
            _attributes = new Dictionary<string, MockAttrib>();
        }

        public override AttributeNameEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Deprovision()
        {
            throw new NotImplementedException();
        }

        public override void CommitNewConnector()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override ReferenceValue DN { get; set; }
        public override string RDN { get; set; }
        public override ValueCollection ObjectClass { get; set; }
        public override string ObjectType { get; }

        public override Attrib this[string attributeName]
        {
            get
            {
                if (!_attributes.ContainsKey(attributeName))
                    _attributes.Add(attributeName, new MockAttrib(attributeName));

                return _attributes[attributeName];
            }
        }

        public override ConnectionState ConnectionState { get; }
        public override ManagementAgent MA { get; }
        public override RuleType ConnectionRule { get; }
        public override DateTime ConnectionChangeTime { get; }
    }
}
