using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.EndPoints.Mvc.Task.Models;
using System.Threading;

namespace SunService.EndPoints.Mvc.Task.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly IHomeServiceAppServices _homeServiceAppServices;
        private readonly IorderAppServices _orderAppServices;
        private readonly UserManager<User> _userManager;
        public OrderController(IHomeServiceAppServices homeServiceAppServices, IorderAppServices orderAppServices, UserManager<User> userManager)
        {
            _homeServiceAppServices = homeServiceAppServices;
            _orderAppServices = orderAppServices;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {

            var homeService = await _homeServiceAppServices.GetAllHomeService(cancellationToken);

            var viewmodel = new OrderViewModel
            {
               
                HomeServices=homeService,
                orderDto=new OrderDto()

            };
            return View(viewmodel);

        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model, CancellationToken cToken)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var id = int.Parse(userId);
            model.orderDto.CustomerId = id;

            var order = new OrderDto 
            {
                Id=model.orderDto.Id,
                CreateAt=model.orderDto.CreateAt,
                CustomerFullName=model.orderDto.CustomerFullName,
                HomeserviceId=model.orderDto.HomeserviceId,
                CustomerId=model.orderDto.CustomerId,
                Description=model.orderDto.Description,
                ImplementationDate=model.orderDto.ImplementationDate,
                HomeServiceTitle=model.orderDto.HomeServiceTitle,
                ExpertId=model.orderDto.ExpertId,
                Images=model.orderDto.Images,
                ImplementationTime=model.orderDto.ImplementationTime,
                OfferId=model.orderDto.OfferId,
                Offers=model.orderDto.Offers,
              
                OrderHomeServiceStatus =model.orderDto.OrderHomeServiceStatus,
            };

            
           var result = await _orderAppServices.CreateOrder(order, cToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.IsMessage;
                

            }
            else
            {
                TempData["ErrorMessage"] = result.IsMessage;

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

