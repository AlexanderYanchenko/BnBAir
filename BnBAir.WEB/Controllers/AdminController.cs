using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.WEB.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServiceUW _service;

        public AdminController(IServiceUW service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var reservations =  GetReservationMapper()
                                    .Map<IEnumerable<ReservationDTO>, List<ReservationModel>>(await _service.ReservationsDTO.Get()) 
                                ?? throw new ArgumentNullException();
            return View(reservations);
        }
        
        public async Task<IActionResult>  GetReport()
        {
            var reservations = GetReservationMapper()
                .Map<IEnumerable<ReservationDTO>, List<ReservationModel>>( await _service.ReservationsDTO.Get());
            var report = new ReportModel()
            {
                CountOfReservations = reservations.Count,
                TotalSum = reservations.Sum(reservation 
                    => reservation.Room.Category.CategoryDates.First().Price)
            };
            return View(report);
        }
        
        public async Task<IActionResult> GuestMonitor()
        {
            
            var guests = GetGuestMapper().Map<List<GuestDTO>, List<GuestModel>>(await _service.GuestsDTO.Get());
            return View(guests);
        }

        public async Task<IActionResult> GuestInfo(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id пользователя не может быть пустым");
            }
            var guestReservation = GetGuestMapper()
                .Map<GuestDTO, GuestModel>(await _service.GuestsDTO.GetById(id));
            if (guestReservation == null)
            {
                return NotFound("Пользователь не найден");    
            }

            ViewBag.Reservations = GetReservationMapper()
                .Map<List<ReservationDTO>, List<ReservationModel>>(await _service.ReservationsDTO.Get())
                .Where(x => x.Guest.GuestId == id);
            return View(guestReservation);
        }

        public async Task<IActionResult> ChangeParametersForGuestReservation(Guid id)
        {
            var reservation = GetReservationMapper().Map<ReservationDTO, ReservationModel>(await _service.ReservationsDTO.GetById(id));
            return View(reservation);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeParametersForGuestReservation(Guid id, bool? checkIn, bool? checkOut)
        {
            
            var reservation = GetReservationMapper().Map<ReservationDTO, ReservationModel>(await _service.ReservationsDTO.GetById(id));
            if (checkIn != null) reservation.CheckIn = (bool) checkIn;
            if (checkOut != null) reservation.CheckOut = (bool) checkOut;


            var reversedReservation = GetReservationMapper().Map<ReservationModel, ReservationDTO>(reservation);
            _service.ReservationsDTO.Update(reversedReservation);
            return RedirectToAction(GuestMonitor().ToString());
        }
        private static IMapper GetReservationMapper()
        {
            var mapper = new MapperConfiguration(cfg
                    =>
                {
                    cfg.CreateMap<ReservationDTO, ReservationModel>()
                        .ForMember(x
                            => x.Guest, opt
                            => opt.MapFrom(x => x.Guest))
                        .ForMember(x
                            => x.Room, opt
                            => opt.MapFrom(x => x.Room))
                        .ReverseMap();
                    cfg.CreateMap<GuestDTO, GuestModel>().ReverseMap();
                    cfg.CreateMap<RoomDTO, RoomModel>()
                        .ForMember(x
                            =>x.Category,opt
                            =>opt.MapFrom(x=>x.Category))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>()
                        .ForMember(x=>x.CategoryDates, opt
                            =>opt.MapFrom(x=>x.CategoryDates))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDateDTO, CategoryDateModel>().ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }
        
        private static IMapper GetGuestMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GuestDTO, GuestModel>().ForMember(x
                        =>x.Reservations, opt
                        =>opt.MapFrom(x=>x.Reservations))
                    .ReverseMap();
                cfg.CreateMap<ReservationDTO, ReservationModel>()
                    .ForMember(x=>x.Room, 
                        opt=> opt.MapFrom(x=>x.Room))
                    .ReverseMap();
                cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }
    }
}