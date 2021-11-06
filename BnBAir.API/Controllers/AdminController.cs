using System;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IService<ReservationDTO> _reservations;
        private readonly IService<CategoryDTO> _categories;
        private readonly IService<RoomDTO> _rooms;
        private readonly IBnBAirUW _db;
        public AdminController(IService<ReservationDTO> reservations, IBnBAirUW db, IService<CategoryDTO> categories, IService<RoomDTO> rooms)
        {
            _reservations = reservations;
            _db = db;
            _categories = categories;
            _rooms = rooms;
        }

        [HttpGet("monitoring")]
        public IActionResult MonitorBooking()
        {
            return Ok(_db.Reservations.GetAll());
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
               return Ok(_db.Reservations.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest("Пользователь с таким Id не найден");
            }
        }

        [HttpPost("changeparameters")]
        public IActionResult ChangeParametersForGuest(Guid id)
        {
            var guest = _reservations.GetById(id);
            _reservations.Update(guest);
            _db.Save();
            return Ok("Данные успешно обновлены");
        }

        [HttpPost("addroom")]
        public IActionResult AddRoom(RoomDTO room)
        {
            _rooms.Create(room);
            _db.Save();
            return Ok("Комната добавлена успешно");
        }

        [HttpPost("editroom")]
        public IActionResult EditRoom(RoomDTO room)
        {
            _rooms.Update(room);
            _db.Save();
            return Ok("Комната изменена успешно");
        }
        
        [HttpPost("deleteroom")]
        public IActionResult DeleteRoom(RoomDTO room)
        {
            _rooms.Delete(room);
            _db.Save();
            return Ok("Комната удалена успешно");
        }
        
        [HttpPost("addcategory")]
        public IActionResult AddCategory(CategoryDTO category)
        {
            _categories.Create(category);
            _db.Save();
            return Ok("Категория добавлена успешно");
        }

        [HttpPost("editcategory")]
        public IActionResult EditCategory(CategoryDTO category)
        {
            _categories.Update(category);
            _db.Save();
            return Ok("Категория изменена успешно");
        }
        
        [HttpPost("deletecategory")]
        public IActionResult DeleteCategory(CategoryDTO category)
        {
            _categories.Delete(category);
            _db.Save();
            return Ok("Категория удалена успешно");
        }


        
    }
}