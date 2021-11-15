using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BnBAir.API.Interfaces;
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
    public class AdminController : ControllerBase, IAdmin
    {
        private readonly IServiceUW _service;

        public AdminController(IServiceUW service)
        {
            _service = service;
        }

        [HttpGet("report")]
        public async Task<IActionResult>  GetReport()
        {
            var reservations = GetReservationMapper()
                .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>( await _service.ReservationsDTO.Get());
            var report = new ReportViewModel()
            {
                CountOfReservations = reservations.Count,
                TotalSum = reservations.Sum(reservation 
                    => reservation.Room.Category.CategoryDates.First().Price)
            };
            return Ok(report);
        }
        [HttpGet("monitoring")]
        public async Task<IActionResult> MonitorBooking()
        {

            var reservations =  GetReservationMapper()
                                   .Map<IEnumerable<ReservationDTO>, List<ReservationViewModel>>(await _service.ReservationsDTO.Get()) 
                               ?? throw new ArgumentNullException();
            return Ok(reservations);
        }
        
        [HttpGet("guestmonitor")]
        public async Task<IActionResult> GuestMonitor(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id пользователя не может быть пустым");
            }
            
            var guestReservation = GetGuestMapper()
                .Map<GuestDTO, GuestViewModel>(await _service.GuestsDTO.GetById(id));
            if (guestReservation == null)
            {
                return NotFound("Пользователь не найден");    
            }
            return Ok(guestReservation);
        }

        [HttpPost("changeparameters")]
        public async Task<IActionResult> ChangeParametersForGuest(Guid id, bool? checkIn, bool? checkOut)
        {
            
            var reservation = GetReservationMapper().Map<ReservationDTO, ReservationViewModel>(await _service.ReservationsDTO.GetById(id));
            if (checkIn != null) reservation.CheckIn = (bool) checkIn;
            if (checkOut != null) reservation.CheckOut = (bool) checkOut;


            var reversedReservation = GetReservationMapper().Map<ReservationViewModel, ReservationDTO>(reservation);
            _service.ReservationsDTO.Update(reversedReservation);
            return Ok("Данные успешно обновлены");
        }

        #region Add/Edit/Delete Room
        
        [HttpPost("addroom")]
        public async Task<IActionResult> AddRoom(int number, Guid categoryId)
        {
            var room = new RoomViewModel()
            {
                Number = number,
            };
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(room);
            _service.RoomsDTO.Create(roomDto, categoryId);
            return Ok("Комната добавлена успешно");
        }
        
        [HttpPost("editroom")]
        public async Task<IActionResult> EditRoom(Guid roomId, int? number, Guid? categoryId)
        {
            var roomViewModel = GetRoomMapper().Map<RoomDTO, RoomViewModel>(await _service.RoomsDTO.GetById(roomId));
            CategoryViewModel categoryViewModel = null;
            if (categoryId != null)
            {
                categoryViewModel = GetCategoryMapper()
                    .Map<CategoryDTO, CategoryViewModel>(await _service.CategoriesDTO.GetById((Guid) categoryId));
            }
            
            if (number != null)
            {
                roomViewModel.Number = (int) number;
            }
            
            roomViewModel.Category = categoryViewModel;
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(roomViewModel);
            _service.RoomsDTO.Update(roomDto);
            return Ok("Комната изменена успешно");
        }
        
        [HttpPost("deleteroom")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var room = GetRoomMapper().Map<RoomDTO, RoomViewModel>( await _service.RoomsDTO.GetById(id));
            var roomDto = GetRoomMapper().Map<RoomViewModel, RoomDTO>(room);
            _service.RoomsDTO.Delete(roomDto);
            return Ok("Комната удалена успешно");
        }
        
        #endregion

        #region Add/Edit/Delete Category

        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory(string name, int countOfBed, Guid categoryDatesId)
        {
            var category = new CategoryViewModel()
            {
                Name = name,
                Bed = countOfBed
            };
            var categoryDto = GetCategoryMapper().Map<CategoryViewModel, CategoryDTO>(category);
            _service.CategoriesDTO.Create( categoryDto, categoryDatesId);
            return Ok("Категория добавлена успешно");
        }
        
        [HttpPost("editcategory")]
        public async Task<IActionResult> EditCategory( Guid categoryId, string? name, int? countOfBed, Guid? categoryDatesId)
        {
            var categoryViewModel = GetCategoryMapper().Map<CategoryDTO, CategoryViewModel>(await _service.CategoriesDTO.GetById(categoryId));
            if (name != null)
            {
                categoryViewModel.Name = name;
            }

            if (countOfBed != null)
            {
                categoryViewModel.Bed = (int) countOfBed;
            }

            if (categoryDatesId != null)
            {
               //TODO: Добавить изменение категории
            }
            var categoryDto = GetCategoryMapper().Map<CategoryViewModel, CategoryDTO>(categoryViewModel);
            _service.CategoriesDTO.Update(categoryDto);
            return Ok("Категория изменена успешно");
        }
        
        [HttpPost("deletecategory")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = _service.CategoriesDTO.GetById(id);
            _service.CategoriesDTO.Delete(await category);
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
                cfg.CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }

        private static IMapper GetCategoryMapper()
        {
            var mapper = new MapperConfiguration(cfg=>
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

        private static IMapper GetGuestMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GuestDTO, GuestViewModel>().ForMember(x
                    =>x.Reservations, opt
                    =>opt.MapFrom(x=>x.Reservations))
                    .ReverseMap();
                cfg.CreateMap<ReservationDTO, ReservationViewModel>()
                    .ForMember(x=>x.Room, 
                        opt=> opt.MapFrom(x=>x.Room))
                    .ReverseMap();
                cfg.CreateMap<RoomDTO, RoomViewModel>().ReverseMap();
            }).CreateMapper();
            return mapper;
        }
        #endregion
       
    }
}