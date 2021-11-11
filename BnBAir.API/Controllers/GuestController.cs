﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
   // [Authorize(Roles = "guest")]
    public class GuestController : ControllerBase
    {
        private readonly IServiceUW _service;
        public GuestController(IServiceUW service)
        {
            _service = service;
        }

        [HttpGet("listofrooms")]
        public async Task<IActionResult> ListOfRooms()
        {
            var rooms = GetRoomMapper()
                .Map<IEnumerable<RoomDTO>, List<RoomViewModel>>( await _service.RoomsDTO.Get());
            return Ok(rooms);
        }
        [HttpGet("searchrooms")]
        public async Task<IActionResult> SearchRoomsForDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Дата заселения не может быть дальше, чем дата выселения");
            }

            var emptyRooms = new List<RoomViewModel>();
            var reservations = GetReservationMapper()
                .Map<List<ReservationDTO>, List<ReservationViewModel>>(  await _service.ReservationsDTO.Get());
            var rooms = GetRoomMapper().Map<List<RoomDTO>, List<RoomViewModel>>(await _service.RoomsDTO.Get());

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
            
            emptyRooms.AddRange(rooms.Where(room => !reservations.Exists(x => x.Room.RoomId == room.RoomId)));
            
            if (!emptyRooms.Any())
            {
                return BadRequest($"Свободных комнат на дату {startDate} - {endDate} нет");
            }

            return Ok(emptyRooms);
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

        private static IMapper GetRoomMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
            {
                cfg.CreateMap<RoomDTO, RoomViewModel>().ForMember(x
                        =>x.Category,opt
                        =>opt.MapFrom(x=>x.Category))
                    .ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryViewModel>().ForMember(x
                    => x.CategoryDates, opt
                        =>opt.MapFrom(x=>x.CategoryDates))
                    .ReverseMap();
                cfg.CreateMap<CategoryDateDTO, CategoryDatesViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        #endregion

    }
}