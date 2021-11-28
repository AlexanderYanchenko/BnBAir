using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BnBAir.WEB.Controllers
{
    public class GuestController : Controller
    {
        private readonly IServiceUW _service;
        public GuestController(IServiceUW service)
        {
            _service = service;
        }

        public IActionResult BookRoom(Guid id)
        {
            ViewBag.Room = _service.RoomsDTO.GetById(id).Result.Number;
            var rooms = _service.RoomsDTO.Get();
            var sortedRooms = rooms.Result.OrderByDescending(r => r.RoomId);
            ViewBag.Rooms = new SelectList(sortedRooms, "RoomId", "Number");
            return View();
        }
        
        [HttpPost]
        public IActionResult BookRoom(ReservationModel reservation)
        {
            var reservationDto = GetReservationMapper().Map<ReservationModel, ReservationDTO>(reservation);
            var roomId = reservationDto.Room.RoomId;
            _service.ReservationsDTO.Create(reservationDto, roomId);
            return Ok("Бронирование успешно!");
        }
        
        #region Mappers

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
        #endregion
    }
}