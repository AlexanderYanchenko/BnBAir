using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore.Query;

namespace BnBAir.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        T GetById(Guid id);
        void Create(T item, Guid itemId);
        void Update(T item);
        void Delete(Guid id);
    }
}