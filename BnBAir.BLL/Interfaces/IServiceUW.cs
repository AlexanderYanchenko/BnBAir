using System;
using BnBAir.BLL.DTO;

namespace BnBAir.BLL.Interfaces
{
    public interface IServiceUW : IDisposable
    {
        IService<CategoryDateDTO> CategoryDatesDTO { get; }
        IService<CategoryDTO> CategoriesDTO { get; }
        IService<GuestDTO> GuestsDTO { get; }
        IService<ReservationDTO> ReservationsDTO { get; }
        IService<RoomDTO> RoomsDTO { get; }
    }
}