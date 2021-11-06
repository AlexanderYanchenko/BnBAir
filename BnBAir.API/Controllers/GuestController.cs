using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IService<ReservationDTO> _reservations;
        private readonly IBnBAirUW _db;
        public GuestController(IService<ReservationDTO> reservationsDto, IBnBAirUW db)
        {
            _reservations = reservationsDto;
            _db = db;
        }

        [HttpGet("searchrooms")]
        public IActionResult SearchRoomsForDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Дата заселения не может быть дальше, чем дата выселения");
            }

            var emptyRooms = new List<Room>();
            var reservations = _db.Reservations.GetAll();

            foreach (var res in reservations)
            {
                var dateRange = new List<DateTime>();
                var startDateCheck = res.StartDate;
                var endDateCheck = res.EndDate;
                while (startDateCheck <= endDateCheck)
                {
                    dateRange.Add(startDateCheck);
                    startDateCheck = startDateCheck.AddDays(1);
                }
                var startDateContain = dateRange.Contains(startDate);
                var endDateContain = dateRange.Contains(endDate);
                if (!startDateContain && !endDateContain)
                {
                    emptyRooms.Add(res.Room);
                }
            }
            
            if (!emptyRooms.Any())
            {
                return BadRequest($"Свободных комнат на дату {startDate} - {endDate} нет");
            }

            return Ok(emptyRooms);
        }

        [HttpPost]
        [Route("Booking")]
        public IActionResult BookRoom(ReservationDTO reservation)
        {
            _reservations.Create(reservation);
            return Ok();
        }

        public IMapper CreateMapper()
        {
            var mapper = new MapperConfiguration(cfg
                => cfg.CreateMap<ReservationDTO, ReservationViewModel>()).CreateMapper();
            return mapper;
        }
    }
}