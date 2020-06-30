﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NSI_CRK.DAL
{
    public class GenericRepository<Type> : IGenericRepository<Type> where Type : class
    {
        public CRKContext crkContext = null;
        public DbSet<Type> dbSet = null;

        public GenericRepository()
        {
            this.crkContext = new CRKContext();
            dbSet = crkContext.Set<Type>();
        }
        public GenericRepository(CRKContext context)
        {
            this.crkContext = context;
            dbSet = context.Set<Type>();
        }

        public IEnumerable<Type> GetAll()
        {
            return dbSet.ToList();
        }

        public Type GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Type obj)
        {
            dbSet.Add(obj);
        }

        public void Update(Type obj)
        {
            dbSet.Attach(obj);
            crkContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Type existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }

        public void Save()
        {
            crkContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    crkContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}