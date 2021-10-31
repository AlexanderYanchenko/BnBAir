using System;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Interfaces
{
    public interface IBnBAirUW : IDisposable
    {
         IRepository<CategoryDate> CategoryDates { get;}
         IRepository<Category> Categories { get;}
         IRepository<Guest> Guests { get;}
         IRepository<Reservation> Reservations { get;}
         IRepository<Room> Rooms { get;}

    }
}