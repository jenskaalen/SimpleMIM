using System;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public class MockConnectors: ConnectorCollection
    {
        private int _count;


        public override CSEntry StartNewConnector(string objectType, ValueCollection objectClass)
        {
            _count++;
            return null;
        }

        public override CSEntry StartNewConnector(string objectType, string[] objectClass)
        {
            _count++;
            return null;
        }

        public override CSEntry StartNewConnector(string objectType)
        {
            _count++;
            return null;
        }

        public override void DeprovisionAll()
        {
            _count--;
        }

        public override ConnectorCollectionEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get { return _count; }
        }

        public override ConnectorCollectionByDN ByDN { get; }
        public override ConnectorCollectionByIndex ByIndex { get; }
    }
}