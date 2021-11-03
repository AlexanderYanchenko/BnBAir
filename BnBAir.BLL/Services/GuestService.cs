using System;
using System.Collections.Generic;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;

namespace BnBAir.BLL.Services
{
    public class GuestService : IService<GuestDTO>
    {
        private readonly IBnBAirUW _db;
        private readonly IMapper _mapper = CreateMapper();

        public GuestService(IBnBAirUW db)
        {
            _db = db;
        }


        public List<GuestDTO> Get()
        {
            return _mapper.Map<IEnumerable<Guest>, List<GuestDTO>>(_db.Guests.GetAll());
        }

        public GuestDTO GetById(Guid id)
        {
            return _mapper.Map<Guest, GuestDTO>(_db.Guests.GetById(id));
        }


        public void Update(GuestDTO model)
        {
            var guest = _mapper.Map<GuestDTO, Guest>(model);
            _db.Guests.Update(guest);
            _db.Save();
        }

        public void Create(GuestDTO model)
        {
            var guest = _mapper.Map<GuestDTO, Guest>(model);
            _db.Guests.Create(guest);
            _db.Save();
        }

        public void Delete(GuestDTO model)
        {
            var guest = _mapper.Map<GuestDTO, Guest>(model);
            _db.Guests.Delete(guest.GuestId);
            _db.Save();
        }
        
        private static IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}