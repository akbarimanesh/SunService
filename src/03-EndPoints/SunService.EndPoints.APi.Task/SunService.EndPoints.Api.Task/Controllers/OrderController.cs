using Microsoft.AspNetCore.Mvc;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.Task.Configs;
using SunService.EndPoints.Api.Task.ApiFramework;
using SunService.EndPoints.Api.Task.ApiFramework.Fillters;

namespace SunService.EndPoints.Api.Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IorderAppServices _orderappServices;
        private readonly SiteSettings _siteSettings;
        public OrderController(IorderAppServices orderappServices, SiteSettings siteSettings)
        {
            _orderappServices = orderappServices;
            _siteSettings = siteSettings;
        }

        [HttpGet("expert/{expertId}")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))] 
        public async Task<IActionResult> GetAllOrderHomeserviceExpert(int expertId, CancellationToken cancellationToken)
        {
            

            var orders = await _orderappServices.GetAllOrderHomeserviceExpert(expertId, cancellationToken);
            if (orders == null || !orders.Any())
            {
                return NotFound(new { Message = "هیچ سفارشی یافت نشد." });
            }

            var filteredOrders = orders.Select(o => new OrderModel
            {
                Id = o.Id,
                CustomerFullName = o.CustomerFullName,
                HomeServiceTitle = o.HomeServiceTitle,
                CreateAt = o.CreateAt.ToShamsi(),
                ImplementationDate = o.ImplementationDate.ToShamsi(),
               
                Description = o.Description,
                OrderHomeServiceStatus = o.OrderHomeServiceStatus,
                CityId = o.CityId,
                ImageUrls = o.ImageUrls
            }).ToList();

            return Ok(filteredOrders);
        }
    }

    }


