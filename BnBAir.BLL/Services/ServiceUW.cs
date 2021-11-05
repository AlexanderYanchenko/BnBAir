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
        private readonly RoomRepository _dbSet;
        private IService<RoomDTO> _roomService;
        private IService<CategoryDTO> _categoryService;
        private IService<CategoryDateDTO> _categoryDatesService;
        private IService<GuestDTO> _guestService;
        private IService<ReservationDTO> _reservationService;
        public ServiceUW(IBnBAirUW db, RoomRepository dbSet)
        {
            _db = db;
            _dbSet = dbSet;
        }

        public IService<RoomDTO> Rooms => _roomService ??= new RoomService(_db, _dbSet);
        public IService<CategoryDTO> Categories => _categoryService ??= new CategoryService(_db);
        public IService<CategoryDateDTO> CategoryDates => _categoryDatesService ??= new CategoryDateService(_db);
        public IService<GuestDTO> Guests => _guestService ??= new GuestService(_db);
        public IService<ReservationDTO> Reservations => _reservationService ??= new ReservationService(_db);

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

       
    }
}