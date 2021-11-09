using System;
using System.Collections.Generic;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;

namespace BnBAir.BLL.Services
{
    public class RoomService  : IService<RoomDTO>
    {
        private readonly IBnBAirUW _db;
        private readonly IMapper _mapper = CreateMapper();

        public RoomService(IBnBAirUW db)
        {
            _db = db;
        }


        public List<RoomDTO> Get()
        {
            return _mapper.Map<IEnumerable<Room>, List<RoomDTO>>(_db.Rooms.GetAll());
        }

        public RoomDTO GetById(Guid id)
        {
            return _mapper.Map<Room, RoomDTO>(_db.Rooms.GetById(id));
        }

        public void Create(RoomDTO model)
        {
            var room = _mapper.Map<RoomDTO, Room>(model);
            _db.Rooms.Create(room);
            _db.Save();
        }

        public void Update(RoomDTO model)
        {
            var room = _mapper.Map<RoomDTO, Room>(model);
            _db.Rooms.Update(room);
            _db.Save();
        }

        public void Delete(RoomDTO model)
        {
            var room = _mapper.Map<RoomDTO, Room>(model);
            _db.Rooms.Delete(room.RoomId);
            _db.Save();
        }
        private static IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
            {
                cfg.CreateMap<Room, RoomDTO>().ForMember(x
                    =>x.Category,opt
                    =>opt.MapFrom(x=>x.Category))
                    .ReverseMap();
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}