using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BnBAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : GenericController<CategoryDTO>
    {
        public CategoryController(IService<CategoryDTO> dbService, IServiceUW db) : base(dbService,db)
        {
        }
        
    }
}