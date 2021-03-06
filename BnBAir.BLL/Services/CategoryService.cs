using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;

namespace BnBAir.BLL.Services
{
    public class CategoryService : IService<CategoryDTO>
    {
        private readonly IBnBAirUW _db;
        private readonly IMapper _mapper = CreateMapper();

        public CategoryService(IBnBAirUW db)
        {
            _db = db;
        }
        
        public async Task<List<CategoryDTO>> Get()
        {
            return _mapper.Map<IEnumerable<Category>, List<CategoryDTO>>( await _db.Categories.GetAll());
        }

        public async Task<CategoryDTO>  GetById(Guid id)
        {
            return _mapper.Map<Category, CategoryDTO>(await _db.Categories.GetById(id));
        }

        public void Create(CategoryDTO model, Guid itemId)
        {
            var category = _mapper.Map<CategoryDTO, Category>(model);
            _db.Categories.Create(category,itemId);
            _db.Save();

        }

        public void Update(CategoryDTO model)
        {
            var category = _mapper.Map<CategoryDTO, Category>(model);
            _db.Categories.Update(category);
            _db.Save();
        }

        public void Delete(CategoryDTO model)
        {
            var category = _mapper.Map<CategoryDTO, Category>(model);
            _db.Categories.Delete(category.CategoryId);
            _db.Save();
        }
        
        private static IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
                {
                    cfg.CreateMap<Category, CategoryDTO>()
                        .ForMember(x
                            => x.CategoryDates, opt
                            => opt.MapFrom(x => x.CategoryDates))
                        .ForMember(x=>x.Rooms, opt
                        =>opt.MapFrom(x=>x.Rooms))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDate, CategoryDateDTO>()
                        .ReverseMap();
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}