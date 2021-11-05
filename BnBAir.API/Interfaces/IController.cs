using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Interfaces
{
    public interface IController<T>  where T : class
    {
        public List<T> Get();
        public T GetById(Guid id);
    }
}