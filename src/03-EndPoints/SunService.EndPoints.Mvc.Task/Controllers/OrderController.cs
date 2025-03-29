using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.EndPoints.Mvc.Task.Areas.Account.Controllers;
using SunService.EndPoints.Mvc.Task.Models;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : BaseController
    {
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly IorderAppServices _orderAppServices;
        private readonly UserManager<User> _userManager;
        private readonly ICategoryAppServices _categoryAppServices;
        public OrderController(
       IHomeServiceAppServices homeServiceAppServices,
        ICategoryAppServices categoryAppServices,
        IorderAppServices orderAppServices,
        UserManager<User> userManager
      ) : base(categoryAppServices)
        {
            _homeServiceAppServices = homeServiceAppServices;
            _orderAppServices = orderAppServices;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Create( int Id,CancellationToken cancellationToken)
        {
            await SetCategories(cancellationToken);
            var homeService = await _homeServiceAppServices.GetAllHomeService(cancellationToken);

            var viewmodel = new OrderViewModel
            {
               
                HomeServices=homeService,
                orderDto=new OrderDto() { HomeserviceId = Id },

            };
            return View(viewmodel);

        }
        
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model, CancellationToken cToken)
        {
            

            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            model.orderDto.CustomerId = id;
            var user = await _userManager.FindByIdAsync(userId);
            var CityId1 = user?.CityId;
            model.orderDto.CityId = CityId1;

            
            DateTime persianDateTime = model.orderDto.ImplementationDate; 

       
                PersianDateTime persianDate = PersianDateTime.Parse(persianDateTime.ToString("yyyy/MM/dd"));

                
                DateTime gregorianDate = persianDate.ToDateTime(); 

                
                model.orderDto.ImplementationDate = gregorianDate;
          




            var order = new OrderDto ()
            {
                Id=model.orderDto.Id,
                CreateAt=model.orderDto.CreateAt,
             
                HomeserviceId=model.orderDto.HomeserviceId,
                CustomerId=model.orderDto.CustomerId,
                Description=model.orderDto.Description,
                ImplementationDate=model.orderDto.ImplementationDate,
                
                ExpertId =model.orderDto.ExpertId,
                Images=model.orderDto.Images,
                CityId = model.orderDto.CityId,
                ImplementationTime =model.orderDto.ImplementationTime  ,
               OfferId=model.orderDto.OfferId,
            
            
                OrderHomeServiceStatus =model.orderDto.OrderHomeServiceStatus,
            };

            if (ModelState.IsValid)
            {
               
                var result = await _orderAppServices.CreateOrder(order, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;


                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;

                }
            }

           

            var homeService = await _homeServiceAppServices.GetAllHomeService(cToken);

            var viewmodel = new OrderViewModel
            {

                HomeServices = homeService,
                orderDto = new OrderDto()

            };
            return View(viewmodel);
        }
       
        }

    }

