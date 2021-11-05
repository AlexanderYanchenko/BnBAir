using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : GenericController<ReservationDTO>
    {
        public ReservationController(IService<ReservationDTO> dbService,IServiceUW db) : base(dbService, db)
        {
        }
    }
}