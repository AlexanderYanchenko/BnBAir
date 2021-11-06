using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private IService<ReservationDTO> _reservations;

        public AdminController(IService<ReservationDTO> reservations)
        {
            _reservations = reservations;
        }

        [HttpGet("monitoring")]
        public IActionResult MonitorBooking()
        {
            return Ok(_reservations.Get());
        }
    }
}