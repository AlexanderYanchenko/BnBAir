using System;
using BnBAir.BLL.DTO;

namespace BnBAir.BLL.Interfaces
{
    public interface IServiceUW : IDisposable
    {
        IService<CategoryDateDTO> CategoryDates { get; }
        IService<CategoryDTO> Categories { get; }
        IService<GuestDTO> Guests { get; }
        IService<ReservationDTO> Reservations { get; }
        IService<RoomDTO> Rooms { get; }
        
    }
}