using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BnBAir.WEB.Controllers
{
    public class CategoryDatesController : Controller
    {
        private readonly IServiceUW _service;

        public CategoryDatesController(IServiceUW service)
        {
            _service = service;
        }


        public async Task<IActionResult> ListOfCategoryDates()
        {
            var categoryDates = GetMapper().Map<List<CategoryDateDTO>, List<CategoryDateModel>>(await _service.CategoryDatesDTO.Get());
            return View(categoryDates);
        }

        public IActionResult AddCategoryDate()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddCategoryDate(CategoryDateModel categoryDate)
        {
            var itemId = categoryDate.CategoryDateId;
            var categoryDateToDto = GetMapper().Map<CategoryDateModel, CategoryDateDTO>(categoryDate);
            _service.CategoryDatesDTO.Create(categoryDateToDto, itemId);
            return RedirectToAction("ListOfCategoryDates");
        }

        [HttpGet]        
       public async Task<IActionResult> EditCategoryDate(Guid id)
       {
           var categoryDate = GetMapper().Map<CategoryDateDTO, CategoryDateModel>(await _service.CategoryDatesDTO.GetById(id));
           return View(categoryDate);
       }
        [HttpPost]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCategoryDate(Guid id, decimal? price,DateTime? startDate, DateTime? endDate)
        {
            
            var categoryDate = GetMapper().Map<CategoryDateDTO, CategoryDateModel>(await _service.CategoryDatesDTO.GetById(id));
            if (price != null) categoryDate.Price = (decimal) price;
            if (endDate != null) categoryDate.EndDate = (DateTime) endDate;
            if (startDate != null) categoryDate.StartDate = (DateTime) startDate;
            var categoryDateToDto = GetMapper().Map<CategoryDateModel, CategoryDateDTO>(categoryDate);
            _service.CategoryDatesDTO.Update(categoryDateToDto);
            return RedirectToAction("ListOfCategoryDates");
        }
        
        [HttpPost]
      //  [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCategoryDate(Guid id)
        {
            var categoryDate = GetMapper().Map<CategoryDateDTO, CategoryDateModel>( await _service.CategoryDatesDTO.GetById(id));
            var categoryDateToDto = GetMapper().Map<CategoryDateModel, CategoryDateDTO>(categoryDate);
            _service.CategoryDatesDTO.Delete(categoryDateToDto);
            return RedirectToAction("ListOfCategoryDates");
        }
      
      private static IMapper GetMapper()
      {
          var mapper = new MapperConfiguration(cfg
              =>
          {
              cfg.CreateMap<CategoryDateDTO, CategoryDateModel>()
                  .ForMember(x =>x.Category,
                      opt=>opt.MapFrom(x=>x.Category))
                  .ReverseMap();
              cfg.CreateMap<CategoryDTO, CategoryModel>()
               .ReverseMap();
          }).CreateMapper();
          return mapper;
      }
    }
}