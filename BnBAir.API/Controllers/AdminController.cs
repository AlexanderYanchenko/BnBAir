using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceUW _service;
        private readonly IBnBAirUW _db;
        public AdminController(IBnBAirUW db,IServiceUW service)
        {
            _db = db;
            _service = service;
        }

        [HttpGet("report")]
        public IActionResult GetReport()
        {
            var reservations = GetReservationMapper()
                .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>(_service.ReservationsDTO.Get());
            var report = new ReportViewModel()
            {
                CountOfReservations = reservations.Count,
                TotalSum = reservations.Sum(reservation 
                    => reservation.Room.Category.CategoryDates.First().Price)
            };
            return Ok(report);
        }
        [HttpGet("monitoring")]
        public IActionResult MonitorBooking()
        {

            var reservations = GetReservationMapper()
                    .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>(_service.ReservationsDTO.Get()) 
                ?? throw new ArgumentNullException();
            return Ok(reservations);
        }
        
        [HttpGet("guestmonitor")]
        public IActionResult GuestMonitor(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("Id пользователя не может быть пустым");
            }

            try
            {
                var guestReservation = GetReservationMapper()
                    .Map<ReservationDTO, ReservationViewModel>(_service.ReservationsDTO.GetById(id));
               return Ok(guestReservation);
            }
            catch (Exception e)
            {
                return BadRequest("Пользователь с таким Id не найден");
            }
        }

        [HttpPost("changeparameters")]
        public IActionResult ChangeParametersForGuest(Guid id, bool? checkIn, bool? checkOut)
        {
            
            var reservation = GetReservationMapper().Map<ReservationDTO, ReservationViewModel>(_service.ReservationsDTO.GetById(id));
            if (checkIn != null) reservation.CheckIn = (bool) checkIn;
            if (checkOut != null) reservation.CheckOut = (bool) checkOut;


            var reversedReservation = GetReservationMapper().Map<ReservationViewModel, ReservationDTO>(reservation);
            _service.ReservationsDTO.Update(reversedReservation);
            return Ok("Данные успешно обновлены");
        }

        #region Add/Edit/Delete Room
        
        [HttpPost("addroom")]
        public IActionResult AddRoom(RoomViewModel room)
        {
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(room);
            _service.RoomsDTO.Create(roomDto);
            return Ok("Комната добавлена успешно");
        }
        
        [HttpPost("editroom")]
        public IActionResult EditRoom(RoomViewModel room)
        {
            var roomViewModel = GetRoomMapper().Map<RoomDTO, RoomViewModel>(_service.RoomsDTO.GetById(room.RoomId));
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(roomViewModel);
            _service.RoomsDTO.Update(roomDto);
            return Ok("Комната изменена успешно");
        }
        
        [HttpPost("deleteroom")]
        public IActionResult DeleteRoom(Guid id)
        {
            var room = GetRoomMapper().Map<RoomDTO, RoomViewModel>(_service.RoomsDTO.GetById(id));
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(room);
            _service.RoomsDTO.Delete(roomDto);
            return Ok("Комната удалена успешно");
        }
        
        #endregion

        #region Add/Edit/Delete Category

        [HttpPost("addcategory")]
        public IActionResult AddCategory(CategoryViewModel category)
        {
            var categoryDto = GetCategoryMapper().Map<CategoryViewModel, CategoryDTO>(category);
            _service.CategoriesDTO.Create(categoryDto);
            return Ok("Категория добавлена успешно");
        }
        
        [HttpPost("editcategory")]
        public IActionResult EditCategory(CategoryViewModel category)
        {
            var categoryViewModel = GetCategoryMapper().Map<CategoryDTO, CategoryViewModel>(_service.CategoriesDTO.GetById(category.CategoryId));
            var categoryDto = GetCategoryMapper().Map<CategoryViewModel, CategoryDTO>(categoryViewModel);
            _service.CategoriesDTO.Update(categoryDto);
            return Ok("Категория изменена успешно");
        }
        
        [HttpPost("deletecategory")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _service.CategoriesDTO.GetById(id);
            _service.CategoriesDTO.Delete(category);
            return Ok("Категория удалена успешно");
        }

        #endregion
        
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
                cfg.CreateMap<RoomDTO, RoomViewModel>().ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        private static IMapper GetRoomMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
            {
                cfg.CreateMap<RoomDTO, RoomViewModel>().ForMember(x
                        =>x.Category,opt
                        =>opt.MapFrom(x=>x.Category))
                    .ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        private static IMapper GetCategoryMapper()
        {
            var mapper = new MapperConfiguration(cfg
                    =>
                {
                    cfg.CreateMap<CategoryDTO, CategoryViewModel>()
                        .ForMember(x
                            => x.CategoryDates, opt
                            => opt.MapFrom(x => x.CategoryDates))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDateDTO, CategoryDatesViewModel>()
                        .ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }
        
        #endregion
       
    }
}