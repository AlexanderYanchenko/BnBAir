using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
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

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public T GetById(Guid id)
        {
            return  _dbSet.Find(id);
        }

        public virtual async void Create(T item, Guid itemId)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async void Update(T item)
        {
            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
            }

            if (item != null) _db.Entry(item).State = EntityState.Added;
            await _db.SaveChangesAsync();
        }

        public virtual void Delete(Guid id)
        {
            _dbSet.Remove(GetById(id));
            _db.SaveChanges();
        }
    }
}