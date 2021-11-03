using System;
using System.Collections.Generic;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.Interfaces
{
    public interface IService<T> : IDisposable where T : class 
    {
        public void Create(T model);
        public List<T> Get();
        public T GetById(Guid id);
        public void Update(T model);
        public void Delete(T model);

    }
}