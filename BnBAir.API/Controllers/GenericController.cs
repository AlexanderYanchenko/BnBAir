using System;
using System.Linq;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    public abstract class GenericController<T> : ControllerBase where T : class
    {
        private readonly IService<T> _dbService;
        private readonly IServiceUW _db;

        public GenericController(IService<T> dbService, IServiceUW db)
        {
            _dbService = dbService;
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_dbService.GetById(id));
        }

    }
}