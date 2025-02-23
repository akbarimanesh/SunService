using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.AppServices.SunServices.HService;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Enums;
using SunService.Domain.Core.SunServices.UserS.Enums;
using SunService.EndPoints.Mvc.Task.Areas.Admin.Models;

namespace SunService.EndPoints.Mvc.Task.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IorderAppServices _orderAppServices;

        public OrdersController(IorderAppServices orderAppServices)
        {
            _orderAppServices = orderAppServices;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var orders = await _orderAppServices.GetAllOrder(cancellationToken);
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cToken)
        {
            var order = await _orderAppServices.GetorderById(id, cToken);
            if (order == null)
            {
                return NotFound();
            }
            var model = new StatusUpdateModel()
            {
                OrderId = order.Id,
                Status = (int)(order.OrderHomeServiceStatus),
                Statuses = GetOrdersList()

            }; 
            return View(model);




        }
        [HttpPost]
        public async Task<IActionResult> Update(StatusUpdateModel model, CancellationToken cToken)
        {
            if (ModelState.IsValid)
            {
                if (model.Status == 0)
                {
                    // در صورت عدم ارسال مقدار صحیح
                    TempData["ErrorMessage"] = "لطفاً وضعیت را انتخاب کنید.";
                    return RedirectToAction("Index", "Orders");
                }
                model.Statuses = GetOrdersList();
                var order1 = new OrderDto { Id = model.OrderId, OrderHomeServiceStatus = (OrderHomeServiceStatusEnum)model.Status };
                var result = await _orderAppServices.UpdateOrderStatus(order1.Id, order1.OrderHomeServiceStatus, cToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.IsMessage;
                }
                else
                {
                    TempData["ErrorMessage"] = result.IsMessage;
                }
                return RedirectToAction("Index", "Orders");
            }
            model.Statuses = GetOrdersList();
            return View(model);

        }
        private List<SelectListItem> GetOrdersList()
        {
            return Enum.GetValues(typeof(OrderHomeServiceStatusEnum))
                .Cast<OrderHomeServiceStatusEnum>()

                .Select(r => new SelectListItem
                {
                    Value = ((int)r).ToString(),
                    Text = r.ToString()
                })
                .ToList();
        }

    }
     
}
