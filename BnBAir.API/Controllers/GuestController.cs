using System;
using System.Collections.Generic;
using System.Linq;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private IService<ReservationDTO> _ReservationsDTO;
        private IService<RoomDTO> _RoomsDTO;
        public GuestController(IService<ReservationDTO> reservationsDto, IService<RoomDTO> roomsDto)
        {
            _ReservationsDTO = reservationsDto;
            _RoomsDTO = roomsDto;
        }

        [HttpGet("searchrooms")]
        public IActionResult SearchRoomsForDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Дата заселения не может быть дальше, чем дата выселения");
            }
            
            var emptyRooms = new List<RoomDTO>();
            var reservations = _ReservationsDTO.Get();
      //      var rooms = _RoomsDTO.Get().Select(r=>r.Number).ToList();
        //    var notEmptyRooms = reservations.Select(res => res.Room).ToList();
            
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
            
      //    emptyRooms.AddRange(rooms.Where(room => notEmptyRooms.Contains() == false));
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
            _ReservationsDTO.Create(reservation);
            return Ok();
        }
    }
}