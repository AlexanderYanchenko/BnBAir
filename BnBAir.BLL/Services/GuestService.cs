using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

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


        public async Task<List<GuestDTO>>  Get()
        {
            return _mapper.Map<IEnumerable<Guest>, List<GuestDTO>>(await _db.Guests.GetAll());
        }

        public async Task<GuestDTO> GetById(Guid id)
        {
            return _mapper.Map<Guest, GuestDTO>( await _db.Guests.GetById(id));
        }
        
        public void Update(GuestDTO model)
        {
            var guest = _mapper.Map<GuestDTO, Guest>(model);
            _db.Guests.Update(guest);
            _db.Save();
        }

        public void Create(GuestDTO model, Guid? itemId)
        {
            var guest = _mapper.Map<GuestDTO, Guest>(model);
            _db.Guests.Create(guest, null);
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
                =>
            {
                cfg.CreateMap<Guest, GuestDTO>().ForMember(x
                    =>x.Reservations, opt
                    =>opt.MapFrom(x=>x.Reservations))
                    .ReverseMap();
                cfg.CreateMap<Reservation, ReservationDTO>()
                    .ForMember(x=>x.Room, 
                        opt=> opt.MapFrom(x=>x.Room))
                    .ReverseMap();
                cfg.CreateMap<Room, RoomDTO>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}