﻿using System;
using System.Collections.Generic;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ReservationContext _db;
        protected DbSet<T> _dbSet;

        public GenericRepository(ReservationContext db, DbSet<T> dbSet)
        {
            _db = db;
            _dbSet = dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return this._dbSet;
        }

        public T GetById(Guid id)
        {
            return this._dbSet.Find(id);
        }

        public void Create(T item)
        {
            this._dbSet.Add(item);
        }

        public void Update(T item)
        {
            this._db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var item = GetById(id);
            if (item != null)
            {
                this._dbSet.Remove(item);
            }
        }
    }
}