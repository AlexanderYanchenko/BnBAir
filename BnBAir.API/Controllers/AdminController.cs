using System;
using System.Collections.Generic;
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
    public class AdminController : ControllerBase
    {
        private readonly IServiceUW _service;
        private readonly IBnBAirUW _db;
        public AdminController(IBnBAirUW db,IServiceUW service)
        {
            _db = db;
            _service = service;
        }
    //    [Authorize(Roles = "admin")]
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
        public IActionResult ChangeParametersForGuest(Guid id)
        {
            var guest = _service.ReservationsDTO.GetById(id);
            _service.ReservationsDTO.Update(guest);
            _db.Save();
            return Ok("Данные успешно обновлены");
        }

        [HttpPost("addroom")]
        public IActionResult AddRoom(RoomDTO room)
        {
            _service.RoomsDTO.Create(room);
            _db.Save();
            return Ok("Комната добавлена успешно");
        }

        [HttpPost("editroom")]
        public IActionResult EditRoom(RoomDTO room)
        {
            _service.RoomsDTO.Update(room);
            _db.Save();
            return Ok("Комната изменена успешно");
        }
        
        [HttpPost("deleteroom")]
        public IActionResult DeleteRoom(RoomDTO room)
        {
            _service.RoomsDTO.Delete(room);
            _db.Save();
            return Ok("Комната удалена успешно");
        }
        
        [HttpPost("addcategory")]
        public IActionResult AddCategory(CategoryDTO category)
        {
            _service.CategoriesDTO.Create(category);
            _db.Save();
            return Ok("Категория добавлена успешно");
        }

        [HttpPost("editcategory")]
        public IActionResult EditCategory(CategoryDTO category)
        {
            _service.CategoriesDTO.Update(category);
            _db.Save();
            return Ok("Категория изменена успешно");
        }
        
        [HttpPost("deletecategory")]
        public IActionResult DeleteCategory(CategoryDTO category)
        {
            _service.CategoriesDTO.Delete(category);
            _db.Save();
            return Ok("Категория удалена успешно");
        }

        public IMapper GetReservationMapper()
        {
            var mapper = new MapperConfiguration(cfg
                    => cfg.CreateMap<ReservationDTO, ReservationViewModel>())
                .CreateMapper();
            return mapper;
        }
        
    }
}