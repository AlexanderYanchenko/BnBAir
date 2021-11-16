using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceUW _service;

        public AdminController(IServiceUW service)
        {
            _service = service;
        }

        [HttpGet("report")]
        public async Task<IActionResult>  GetReport()
        {
            var reservations = GetReservationMapper()
                .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>( await _service.ReservationsDTO.Get());
            var report = new ReportViewModel()
            {
                CountOfReservations = reservations.Count,
                TotalSum = reservations.Sum(reservation 
                    => reservation.Room.Category.CategoryDates.First().Price)
            };
            return Ok(report);
        }
        [HttpGet("monitoring")]
        public async Task<IActionResult> MonitorBooking()
        {

            var reservations =  GetReservationMapper()
                                   .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>(await _service.ReservationsDTO.Get()) 
                               ?? throw new ArgumentNullException();
            return Ok(reservations);
        }
        
        [HttpGet("guestmonitor")]
        public async Task<IActionResult> GuestMonitor(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id пользователя не может быть пустым");
            }
            
            var guestReservation = GetGuestMapper()
                .Map<GuestDTO, GuestViewModel>(await _service.GuestsDTO.GetById(id));
            if (guestReservation == null)
            {
                return NotFound("Пользователь не найден");    
            }
            return Ok(guestReservation);
        }

        [HttpPost("changeparameters")]
        public async Task<IActionResult> ChangeParametersForGuest(Guid id, bool? checkIn, bool? checkOut)
        {
            
            var reservation = GetReservationMapper().Map<ReservationDTO, ReservationViewModel>(await _service.ReservationsDTO.GetById(id));
            if (checkIn != null) reservation.CheckIn = (bool) checkIn;
            if (checkOut != null) reservation.CheckOut = (bool) checkOut;


            var reversedReservation = GetReservationMapper().Map<ReservationViewModel, ReservationDTO>(reservation);
            _service.ReservationsDTO.Update(reversedReservation);
            return Ok("Данные успешно обновлены");
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

        private static IMapper GetGuestMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GuestDTO, GuestViewModel>().ForMember(x
                    =>x.Reservations, opt
                    =>opt.MapFrom(x=>x.Reservations))
                    .ReverseMap();
                cfg.CreateMap<ReservationDTO, ReservationViewModel>()
                    .ForMember(x=>x.Room, 
                        opt=> opt.MapFrom(x=>x.Room))
                    .ReverseMap();
                cfg.CreateMap<RoomDTO, RoomViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }
        #endregion
       
    }
}