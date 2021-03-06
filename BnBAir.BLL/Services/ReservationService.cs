using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.Data.SqlClient;

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


        public async Task<List<ReservationDTO>> Get()
        {
            return _mapper.Map<IEnumerable<Reservation>, List<ReservationDTO>>(await _db.Reservations.GetAll());
        }


        public async Task<ReservationDTO> GetById(Guid id)
        {
            return _mapper.Map<Reservation, ReservationDTO>( await _db.Reservations.GetById(id));
        }
        
        public void Create(ReservationDTO model, Guid itemId)
        {
            var reservation = _mapper.Map<ReservationDTO, Reservation>(model);
            _db.Reservations.Create(reservation,itemId);
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
            _db.Reservations.Delete(reservation.ReservationId);
            _db.Save();
        }

        
        private static IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
                {
                    cfg.CreateMap<Reservation, ReservationDTO>()
                        .ForMember(x
                            => x.Guest, opt
                            => opt.MapFrom(x => x.Guest))
                        .ForMember(x
                            => x.Room, opt
                            => opt.MapFrom(x => x.Room))
                        .ReverseMap();
                    cfg.CreateMap<Guest, GuestDTO>().ReverseMap();
                    cfg.CreateMap<Room, RoomDTO>()
                        .ForMember(x
                            =>x.Category,opt
                            =>opt.MapFrom(x=>x.Category))
                        .ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>()
                        .ForMember(x=>x.CategoryDates, opt
                        =>opt.MapFrom(x=>x.CategoryDates))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDate, CategoryDateDTO>().ReverseMap();
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