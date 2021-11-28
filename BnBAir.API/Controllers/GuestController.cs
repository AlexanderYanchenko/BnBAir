using System;
using AutoMapper;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(Roles = "guest")]
    public class GuestController : ControllerBase
    {
        private readonly IServiceUW _service;
        public GuestController(IServiceUW service)
        {
            _service = service;
        }
        
        [HttpPost("booking")]
        public IActionResult BookRoom(string firstName,string lastName,string patronymic,DateTime birthDate,string document, Guid roomId, DateTime startDate, DateTime endDate)
        {
            var guest = new GuestViewModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                BirthDate = birthDate,
                Document = document     
            };
            var reservation = new ReservationViewModel()
            {
                StartDate = startDate,
                EndDate = endDate,
                Guest = guest,
            };
            var reservationDto = GetReservationMapper().Map<ReservationViewModel, ReservationDTO>(reservation);
            _service.ReservationsDTO.Create(reservationDto, roomId);
            return Ok();
        }
        
        #region Mappers

        private static IMapper GetReservationMapper()
        {
            var mapper = new MapperConfiguration(cfg
                    =>
                {
                    cfg.CreateMap<ReservationDTO, ReservationViewModel>()
                        .ForMember(x
                            => x.Guest, opt
                            => opt.MapFrom(x => x.Guest))
                        .ForMember(x
                            => x.Room, opt
                            => opt.MapFrom(x => x.Room))
                        .ReverseMap();
                    cfg.CreateMap<GuestDTO, GuestViewModel>().ReverseMap();
                    cfg.CreateMap<RoomDTO, RoomViewModel>()
                        .ForMember(x
                            =>x.Category,opt
                            =>opt.MapFrom(x=>x.Category))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryViewModel>()
                        .ForMember(x=>x.CategoryDates, opt
                            =>opt.MapFrom(x=>x.CategoryDates))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDateDTO, CategoryDatesViewModel>().ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }
        #endregion

    }
}