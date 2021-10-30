using System;
using System.Collections.Generic;

namespace BnBAir.BLL.Interfaces
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