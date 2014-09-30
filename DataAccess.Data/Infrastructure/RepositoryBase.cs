﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        private readonly IDbSet<T> dbset;
        private ResourceManagerEntities dataContext;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; private set; }

        protected ResourceManagerEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public virtual T Add(T entity)
        {
            dbset.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where);
        }
    }
}