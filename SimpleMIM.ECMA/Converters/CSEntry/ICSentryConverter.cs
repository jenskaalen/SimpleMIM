using System.Collections.Generic;
using Microsoft.MetadirectoryServices;

namespace SimpleMIM.ECMA.Converters.CSEntry
{
    public interface ICSentryConverter<T> where T: IExternalObject
    {
        T ConvertFromCSentry(CSEntryChange csentry);
        List<T> ConvertFromCSentries(List<CSEntryChange> csentry);
        CSEntryChange ConvertToCSentry(T entity);
        List<CSEntryChange> ConvertToCSentries(List<T> entities);
    }
}