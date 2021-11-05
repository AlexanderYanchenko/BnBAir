using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : GenericController<GuestDTO>
    {
        public GuestController(IService<GuestDTO> dbService) : base(dbService)
        {
        }
    }
}