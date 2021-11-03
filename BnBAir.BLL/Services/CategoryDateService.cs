using System;
using System.Collections.Generic;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;

namespace BnBAir.BLL.Services
{
    public class CategoryDateService : IService<CategoryDateDTO>
    {
        private readonly IBnBAirUW _db;
        public CategoryDateService(IBnBAirUW db)
        {
            this._db = db;
        }

        public void Create(CategoryDateDTO model)
        {
            var mapper = CreateMapper();

            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Create(categoryDate);
        }

        public List<CategoryDateDTO> Get()
        {
            var mapper = CreateMapper();
            return mapper.Map<IEnumerable<CategoryDate>, List<CategoryDateDTO>>(_db.CategoryDates.GetAll());
        }

        public CategoryDateDTO GetById(Guid id)
        {
            var mapper = CreateMapper();
            return mapper.Map<CategoryDate, CategoryDateDTO>(_db.CategoryDates.GetById(id));
        }

        public void Update(CategoryDateDTO model)
        {
            var mapper = CreateMapper(); 
            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Update(categoryDate);
        }

        public void DeleteCategoryDate(CategoryDateDTO model)
        {
            var mapper = CreateMapper();
            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Update(categoryDate);
        }
        private static IMapper CreateMapper()
        {
             var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<CategoryDate, CategoryDateDTO>()).CreateMapper();
             return mapper;
        }
    }
}