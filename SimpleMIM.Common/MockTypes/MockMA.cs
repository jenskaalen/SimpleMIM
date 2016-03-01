using System;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public class MockMA: ConnectedMA
    {
        public MockMA(string name)
        {
            Connectors = new MockConnectors();
            Name = name;
        }

        public override ReferenceValue CreateDN(string dn)
        {
            throw new NotImplementedException();
        }

        public override ReferenceValue CreateDN(Value dn)
        {
            throw new NotImplementedException();
        }

        public override ReferenceValue EscapeDNComponent(params string[] parts)
        {
            throw new NotImplementedException();
        }

        public override ReferenceValue EscapeDNComponent(params Value[] parts)
        {
            throw new NotImplementedException();
        }

        public override string[] UnescapeDNComponent(string component)
        {
            throw new NotImplementedException();
        }

        public override string NormalizeString(string value)
        {
            throw new NotImplementedException();
        }

        public override string Name { get; }
        public override ConnectorCollection Connectors { get; }
    }
}
