using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NVisionIT.AutomatedTellerMachine.Service.Models
{
    public class DbSet<T> : IDbSet<T> where T : class
    {
        private readonly List<T> list = new List<T>();

        public DbSet()
        {
            list = new List<T>();
        }

        public DbSet(IEnumerable<T> contents)
        {
            list = contents.ToList();
        }

        public ObservableCollection<T> Local => throw new NotImplementedException();

        public Expression Expression
        {
            get { return list.AsQueryable().Expression; }
        }

        public Type ElementType
        {
            get { return list.AsQueryable().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return list.AsQueryable().Provider; }
        }

        public T Add(T entity)
        {
            list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T Remove(T entity)
        {
            list.Remove(entity);
            return entity;
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
