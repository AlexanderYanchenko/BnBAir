using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;

namespace BnBAir.BLL.Services
{
    public class RoomService  : IService<RoomDTO>
    {
        private readonly IBnBAirUW _db;
        private readonly RoomRepository _dbSet;
        private readonly IMapper _mapper = CreateMapper();

        public RoomService(IBnBAirUW db, RoomRepository dbSet)
        {
            _db = db;
            _dbSet = dbSet;
        }


        public List<RoomDTO> Get()
        {
            return _mapper.Map<IEnumerable<Room>, List<RoomDTO>>(_dbSet.GetAll());
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
                => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            return mapper;
        }
        
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}