﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.WEB.Controllers
{
    public class CategoryController : Controller
    {
              private readonly IServiceUW _service;
        public CategoryController(IServiceUW service)
        {
            _service = service;
        }
        
        #region Add/Edit/Delete Category

        [HttpPost("addcategory")]
        public IActionResult AddCategory(string name, int countOfBed, Guid categoryDatesId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя категории не может быть пустым");
            }

            if (countOfBed == 0)
            {
                return BadRequest("Количество кроватей не может быть равным нулю");
            }
            var category = new CategoryModel()
            {
                Name = name,
                Bed = countOfBed
            };
            var categoryDto = GetCategoryMapper().Map<CategoryModel, CategoryDTO>(category);
            _service.CategoriesDTO.Create( categoryDto,categoryDatesId);
            return Ok("Категория добавлена успешно");
        }
        
        [HttpPost("editcategory")]
        public async Task<IActionResult> EditCategory( Guid categoryId, string? name, int? countOfBed, Guid? categoryDatesId)
        {
            var categoryViewModel = GetCategoryMapper().Map<CategoryDTO, CategoryModel>(await _service.CategoriesDTO.GetById(categoryId));
            if (name != null)
            {
                categoryViewModel.Name = name;
            }

            if (countOfBed != null)
            {
                categoryViewModel.Bed = (int) countOfBed;
            }

            if (categoryDatesId != null)
            {
                //TODO: Добавить изменение категории
            }
            var categoryDto = GetCategoryMapper().Map<CategoryModel, CategoryDTO>(categoryViewModel);
            _service.CategoriesDTO.Update(categoryDto);
            return Ok("Категория изменена успешно");
        }
        
        [HttpPost("deletecategory")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = _service.CategoriesDTO.GetById(id);
            if (category == null)
            {
                return BadRequest("Категория не найдена");
            }
            _service.CategoriesDTO.Delete(await category);
            return Ok("Категория удалена успешно");
        }

        #endregion
        
        private static IMapper GetCategoryMapper()
        {
            var mapper = new MapperConfiguration(cfg=>
                {
                    cfg.CreateMap<CategoryDTO, CategoryModel>()
                        .ForMember(x
                            => x.CategoryDates, opt
                            => opt.MapFrom(x => x.CategoryDates))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDateDTO, CategoryDateModel>()
                        .ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }
    }
}