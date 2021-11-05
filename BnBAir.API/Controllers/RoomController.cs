using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : GenericController<RoomDTO>
    {
        public RoomController(IService<RoomDTO> dbService,IServiceUW db) : base(dbService, db)
        {
        }
    }
}