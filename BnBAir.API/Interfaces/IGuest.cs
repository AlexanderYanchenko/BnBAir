using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Interfaces
{
    public interface IGuest
    {
        Task<IActionResult> ListOfRooms();
        Task<IActionResult> SearchRoomsForDate(DateTime startDate, DateTime endDate);
    }
}