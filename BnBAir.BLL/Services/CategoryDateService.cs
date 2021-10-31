using System.Collections.Generic;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;

namespace BnBAir.BLL.Services
{
    public class CategoryDateService
    {
        private IBnBAirUW _db;
        public CategoryDateService(IBnBAirUW db)
        {
            this._db = db;
        }

        public void CreateCategoryDate(CategoryDateDTO model)
        {
            var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<CategoryDate, CategoryDateDTO>()).CreateMapper();

            CategoryDate categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Create(categoryDate);
        }

        public List<CategoryDateDTO> GetCategoryDates()
        {
            var mapper = new MapperConfiguration(cfg
                =>cfg.CreateMap<CategoryDate, CategoryDateDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CategoryDate>, List<CategoryDateDTO>>(_db.CategoryDates.GetAll());
        }
    }
}