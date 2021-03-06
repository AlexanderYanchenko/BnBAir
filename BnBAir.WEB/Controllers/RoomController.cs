using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BnBAir.WEB.Controllers
{
   public class RoomController : Controller
    {
        private readonly IServiceUW _service;

        public RoomController(IServiceUW service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> ListOfRooms()
        {
            var rooms = GetRoomMapper()
                .Map<IEnumerable<RoomDTO>, List<RoomModel>>( await _service.RoomsDTO.Get());
            var sortedRooms = rooms.OrderBy(room => room.Number).ToList();
            foreach (var room in sortedRooms)
            {
                room.Category.CategoryDates = room.Category.CategoryDates
                    .Where(category =>DateTime.Now.Year <= category.EndDate.Year ).ToList();
            }
            return View(sortedRooms);
        }
        [HttpGet]
        public async Task<IActionResult> SearchRoomsForDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Дата заселения не может быть дальше, чем дата выселения");
            }

            var emptyRooms = new List<RoomModel>();
            var reservations = GetReservationMapper()
                .Map<List<ReservationDTO>, List<ReservationModel>>(  await _service.ReservationsDTO.Get());
            var rooms = GetRoomMapper().Map<List<RoomDTO>, List<RoomModel>>(await _service.RoomsDTO.Get());

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
            
            emptyRooms.AddRange(rooms.Where(room => !reservations.Exists(x => x.Room.Number == room.Number)));
            
            if (!emptyRooms.Any())
            {
                return BadRequest($"Свободных комнат на дату {startDate} - {endDate} нет");
            }

            return View(emptyRooms.GroupBy(x=>x.RoomId).Select(x=>x.FirstOrDefault()).ToList());
        }
        
        
        
        #region Add/Edit/Delete Room


        public async Task<IActionResult> AddRoom()
        {
            var categories = GetCategoryMapper()
                .Map<List<CategoryDTO>, List<CategoryModel>>( await _service.CategoriesDTO.Get());
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }
        
        [HttpPost("addroom")]
       // [Authorize(Roles = "admin")]
        public IActionResult AddRoom(int number, Guid categoryId)
        {
            var room = new RoomModel()
            {
                Number = number,
               // Category = GetCategoryMapper().Map<CategoryDTO,CategoryModel>(await _service.CategoriesDTO.GetById(categoryId))
            };
            var roomDto = GetRoomMapper().Map<RoomModel, RoomDTO>(room);
            _service.RoomsDTO.Create(roomDto,categoryId);
            return RedirectToAction("ListOfRooms");
        }

       public async Task<IActionResult> EditRoom(Guid id)
       {
           var room = GetRoomMapper().Map<RoomDTO, RoomModel>(await _service.RoomsDTO.GetById(id));
           var categories = GetCategoryMapper().Map<List<CategoryDTO>, List<CategoryModel>>(await _service.CategoriesDTO.Get()) ;
           ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
           return View(room);
       }
        [HttpPost]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditRoom(Guid id, int? number, Guid? categoryId)
        {
            var roomViewModel = GetRoomMapper().Map<RoomDTO, RoomModel>(await _service.RoomsDTO.GetById(id));
            CategoryModel categoryViewModel = null;
            if (categoryId != null)
            {
                categoryViewModel = GetCategoryMapper()
                    .Map<CategoryDTO, CategoryModel>(await _service.CategoriesDTO.GetById((Guid) categoryId));
            }
            
            if (number != null)
            {
                roomViewModel.Number = (int) number;
            }
            
            roomViewModel.Category = categoryViewModel;
            var roomDto = GetRoomMapper().Map<RoomModel, RoomDTO>(roomViewModel);
            _service.RoomsDTO.Update(roomDto);
            return RedirectToAction("ListOfRooms");
        }
        
        [HttpPost("deleteroom")]
      //  [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var room = GetRoomMapper().Map<RoomDTO, RoomModel>( await _service.RoomsDTO.GetById(id));
            var roomDto = GetRoomMapper().Map<RoomModel, RoomDTO>(room);
            _service.RoomsDTO.Delete(roomDto);
            return RedirectToAction("ListOfRooms");
        }
        
        #endregion
        
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

        private static IMapper GetRoomMapper()
        {
            var mapper = new MapperConfiguration(cfg
                =>
            {
                cfg.CreateMap<RoomDTO, RoomModel>().ForMember(x
                        =>x.Category,opt
                        =>opt.MapFrom(x=>x.Category))
                    .ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryModel>().ForMember(x
                    => x.CategoryDates, opt
                        =>opt.MapFrom(x=>x.CategoryDates))
                    .ReverseMap();
                cfg.CreateMap<CategoryDateDTO, CategoryDateModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }
        
        private static IMapper GetCategoryMapper()
        {
            var mapper = new MapperConfiguration(cfg=>
                {
                    cfg.CreateMap<CategoryDTO, CategoryModel>()
                        .ForMember(x
                            => x.CategoryDates, opt
                            => opt.MapFrom(x => x.CategoryDates))
                        .ForMember(x=>x.Rooms, opt
                            =>opt.MapFrom(x=>x.Rooms))
                        .ReverseMap();
                    cfg.CreateMap<CategoryDateDTO, CategoryDateModel>()
                        .ReverseMap();
                    cfg.CreateMap<RoomDTO, RoomModel>()
                        .ReverseMap();
                })
                .CreateMapper();
            return mapper;
        }

        #endregion
    }
}