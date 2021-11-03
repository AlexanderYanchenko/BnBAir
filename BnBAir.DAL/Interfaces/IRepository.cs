using System;
using System.Collections.Generic;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore.Query;

namespace BnBAir.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}