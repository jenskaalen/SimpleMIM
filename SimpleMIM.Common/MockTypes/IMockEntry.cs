using Microsoft.MetadirectoryServices;

namespace SimpleMIM.Common.MockTypes
{
    public interface IMockEntry
    {
        string ObjectType { get; }
        Attrib this[string attributeName] { get; }
    }
}