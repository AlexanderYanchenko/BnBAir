using System;
using System.Linq;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.DAL.Enitities;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IService<GuestDTO> _guestService;

        public GuestController(IService<GuestDTO> guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public IActionResult GetGuests()
        {
            return Ok(_guestService.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetGuestById(Guid id)
        {
            return Ok(_guestService.GetById(id));
        }
    }
}