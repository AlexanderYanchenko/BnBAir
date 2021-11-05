using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryDatesController : GenericController<CategoryDateDTO>
    {
        public CategoryDatesController(IService<CategoryDateDTO> dbService) : base(dbService)
        {
        }
    }
}