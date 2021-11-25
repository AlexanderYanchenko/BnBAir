using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.Interfaces
{
    public interface IService<T> : IDisposable where T : class 
    {
        public void Create(T model, Guid itemId);
        public Task<List<T>> Get();
        public Task<T> GetById(Guid id);
        public void Update(T model);
        public void Delete(T model);

    }
}