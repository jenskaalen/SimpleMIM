using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public class MockMventry: MVEntry
    {
        private readonly Dictionary<string, MockAttrib> _attributes;

        public MockMventry()
        {
            _attributes = new Dictionary<string, MockAttrib>();
            ConnectedMAs = new MockMACollection();
        }

        public MockMventry(string objectType)
        {
            ObjectType = objectType;
            _attributes = new Dictionary<string, MockAttrib>();
            ConnectedMAs = new MockMACollection();
        }

        public override AttributeNameEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "";
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

    public class AttributeNameEnumMock : AttributeNameEnumerator
    {
        private readonly Dictionary<string, MockAttrib> _dic;
        private IEnumerator _enumerator;
        private int _curIndex;

        public AttributeNameEnumMock(Dictionary<string, MockAttrib> dic)
        {
            _dic = dic;
            _curIndex = -1;
            _enumerator = _dic.Select(pair => pair.Key).ToArray().GetEnumerator();
        }

        public override bool MoveNext()
        {
            //Avoids going beyond the end of the collection.
            if (++_curIndex >= _dic.Count)
            {
                return false;
            }
            else
            {
            }
            return true;
        }

        public override void Reset()
        {
            _curIndex = -1;
        }

        public override string Current
        {
            get { return _dic.ElementAt(_curIndex).Key; }
        }
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