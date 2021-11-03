using System;
using System.Collections.Generic;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;

namespace BnBAir.BLL.Services
{
    public class ReservationService  : IService<ReservationDTO>
    {
        private readonly IBnBAirUW _db;
        private readonly IMapper _mapper = CreateMapper();

        public ReservationService(IBnBAirUW db)
        {
            _db = db;
        }


        public List<ReservationDTO> Get()
        {
            return _mapper.Map<IEnumerable<Reservation>, List<ReservationDTO>>(_db.Reservations.GetAll());
        }


        public ReservationDTO GetById(Guid id)
        {
            return _mapper.Map<Reservation, ReservationDTO>(_db.Reservations.GetById(id));
        }
        
        public void Create(ReservationDTO model)
        {
            var reservation = _mapper.Map<ReservationDTO, Reservation>(model);
            _db.Reservations.Create(reservation);
            _db.Save();
        }

        public void Update(ReservationDTO model)
        {
            var reservation = _mapper.Map<ReservationDTO, Reservation>(model);
            _db.Reservations.Update(reservation);
            _db.Save();
        }

        public void Delete(ReservationDTO model)
        {
            var reservation = _mapper.Map<ReservationDTO, Reservation>(model);
            _db.Guests.Delete(reservation.Guest.GuestId);
            _db.Save();
        }

        
        private static IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<Reservation, ReservationDTO>()).CreateMapper();
            return mapper;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}