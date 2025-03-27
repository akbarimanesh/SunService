using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.EndPoints.Mvc.Task.Models;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryAppServices _categoryAppServices;

        public CategoriesController(ICategoryAppServices categoryAppServices)
        {
            _categoryAppServices = categoryAppServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index( CancellationToken cToken = default)
        {


            var allCategories = await _categoryAppServices.GetAllCategories(cToken);

            
            var viewModel = new HomePageViewModel
            {
                categoryDtos = allCategories,
                
                homeServiceDtos = new List<HomeServiceDto>(), 
                ratingDtos = new List<RatingDto>(), 
                Menu = new MenuViewModel
                {
                    CategoryDtos = allCategories
                }
            };

            return View(viewModel);

           
          

        }

    }
}
