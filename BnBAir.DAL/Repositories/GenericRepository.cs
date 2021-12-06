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

        public virtual async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetById(Guid id) => await _dbSet.FindAsync(id);

        public virtual async void Create(T item, Guid? itemId)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public  void Update(T item)
        {

            _db.Entry(item).State = EntityState.Deleted;
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();

        }

        public virtual async void Delete(Guid id)
        {
            _dbSet.Remove( await GetById(id));
            await _db.SaveChangesAsync();
        }
    }
}