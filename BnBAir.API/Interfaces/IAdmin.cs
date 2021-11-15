using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Interfaces
{
    public interface IAdmin
    {
        Task<IActionResult> MonitorBooking();
    }
}