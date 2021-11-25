using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public async Task< List<CategoryDateDTO>> Get()
        {
            var mapper = CreateMapper();
            return mapper.Map<IEnumerable<CategoryDate>, List<CategoryDateDTO>>( await _db.CategoryDates.GetAll());
        }

        public async Task<CategoryDateDTO> GetById(Guid id)
        {
            var mapper = CreateMapper();
            return mapper.Map<CategoryDate, CategoryDateDTO>( _db.CategoryDates.GetById(id));
        }
        public void Create(CategoryDateDTO model, Guid itemId)
        {
            var mapper = CreateMapper();

            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Create(categoryDate,itemId);
        }

        public void Update(CategoryDateDTO model)
        {
            var mapper = CreateMapper(); 
            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Update(categoryDate);
            _db.Save();
        }

        public void Delete(CategoryDateDTO model)
        {
            var mapper = CreateMapper();
            var categoryDate = mapper.Map<CategoryDateDTO, CategoryDate>(model);
            _db.CategoryDates.Delete(categoryDate.CategoryDateId);
            _db.Save();
        }
        private static IMapper CreateMapper()
        {
             var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<CategoryDate, CategoryDateDTO>()).CreateMapper();
             return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}