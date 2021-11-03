using System;
using System.Linq;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    public abstract class GenericController<T> : ControllerBase where T : class
    {
        private readonly IService<T> _dbService;

        public GenericController(IService<T> dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbService.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_dbService.GetById(id));
        }

    }
}