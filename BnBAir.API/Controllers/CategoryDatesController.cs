using System;
using System.Collections.Generic;
using System.Linq;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.BLL.Services;
using BnBAir.DAL.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryDatesController : ControllerBase
    {
        private readonly IService<CategoryDateDTO> _categoryDateService;

        public CategoryDatesController(IService<CategoryDateDTO> categoryDateService)
        {
            _categoryDateService = categoryDateService;
        }

        [HttpGet]
        public IActionResult GetCategoryDates()
        {
            return Ok(_categoryDateService.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryDateById(Guid id)
        {
            return Ok(_categoryDateService.GetById(id));
        }
        
    }
}