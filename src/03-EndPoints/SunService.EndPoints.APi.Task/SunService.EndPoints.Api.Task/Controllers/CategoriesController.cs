using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.Entities;

namespace SunService.EndPoints.Api.Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoriesController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppServices.GetAllCategoriesWithHomeservice(cancellationToken);
            return Ok(categories);
        }
    }
}
