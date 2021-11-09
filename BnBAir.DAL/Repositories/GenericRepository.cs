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

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async void Create(T item)
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

        public async void Delete(Guid id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _dbSet.Remove(await item);
            }
            await _db.SaveChangesAsync();
        }
    }
}