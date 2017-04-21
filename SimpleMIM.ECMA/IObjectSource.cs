using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMIM.ECMA.Converters;
using SimpleMIM.ECMA.Converters.CSEntry;

namespace SimpleMIM.ECMA
{
    public interface IObjectSource<T> : IDisposable where T: IExternalObject
    {
        string ObjectTypeName { get; }
        Type Type { get; }
        List<T> GetAll();
        void Update(T entity);
        void Add(T entity);
        void Delete(T entity);
        ICSentryConverter<T> CSentryConverter { get; }
    }

    public abstract class ObjectSource<T> : IObjectSource<T> where T : IExternalObject
    {
        public abstract void Dispose();

        public abstract string ObjectTypeName { get; }
        public abstract Type Type { get; }
        public abstract List<T> GetAll();

        public abstract void Update(T entity);

        public abstract void Add(T entity);

        public abstract void Delete(T entity);
        

        public virtual ICSentryConverter<T> CSentryConverter { get; }
    }
}
