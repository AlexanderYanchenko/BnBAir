using System;
using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ReservationContext _db;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(ReservationContext db, DbSet<T> dbSet)
        {
            _db = db;
            _dbSet = dbSet;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _db.SaveChanges();
        }

        public void Update(T item)
        {
            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
            }

            if (item != null) _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _dbSet.Remove(item);
            }
            _db.SaveChanges();
        }
    }
}