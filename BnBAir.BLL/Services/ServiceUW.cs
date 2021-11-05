using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;

namespace BnBAir.BLL.Services
{
    public class ServiceUW : IServiceUW
    {
        private readonly IBnBAirUW _db;
        private IService<RoomDTO> _roomService;
        private IService<CategoryDTO> _categoryService;
        private IService<CategoryDateDTO> _categoryDatesService;
        private IService<GuestDTO> _guestService;
        private IService<ReservationDTO> _reservationService;
        public ServiceUW(IBnBAirUW db)
        {
            _db = db;
        }

        public IService<RoomDTO> RoomsDTO => _roomService ??= new RoomService(_db);
        public IService<CategoryDTO> CategoriesDTO => _categoryService ??= new CategoryService(_db);
        public IService<CategoryDateDTO> CategoryDatesDTO => _categoryDatesService ??= new CategoryDateService(_db);
        public IService<GuestDTO> GuestsDTO => _guestService ??= new GuestService(_db);
        public IService<ReservationDTO> ReservationsDTO => _reservationService ??= new ReservationService(_db);

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

       
    }
}