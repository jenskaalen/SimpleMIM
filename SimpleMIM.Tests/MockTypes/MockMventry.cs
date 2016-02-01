using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MetadirectoryServices;
using MIMSimplifier.Tests.MockTypes;

namespace MIMSimplifier.Tests
{
    public class MockMventry: MVEntry
    {
        private readonly Dictionary<string, MockAttrib> _attributes;

        public MockMventry()
        {
            _attributes = new Dictionary<string, MockAttrib>();
            ConnectedMAs = new MockMACollection();
        }

        public override AttributeNameEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override Guid ObjectID { get; }
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

        public override ConnectedMACollection ConnectedMAs { get; }
    }

    public class MockMACollection : ConnectedMACollection
    {
        private List<ConnectedMA> _connectedMas = new List<ConnectedMA>();

        public override void DeprovisionAll()
        {
            throw new NotImplementedException();
        }

        public override ConnectedMACollectionEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int Count { get; }

        public override ConnectedMA this[string MAName]
        {
            get
            {
                if (!_connectedMas.Any(x => x.Name == MAName))
                    _connectedMas.Add(new MockMA(MAName));

                return _connectedMas.First(ma => ma.Name == MAName);
            }
        }
    }
}