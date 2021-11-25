using System;
using System.Collections.Generic;
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
    }
}