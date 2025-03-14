using Microsoft.Extensions.Caching.Memory;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
using SunService.Domain.Core.SunServices.HService.AppServices;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.HService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.AppServices.SunServices.HService
{
    
    public class HomeServiceAppServices : IHomeServiceAppServices
    {
        private readonly IHomeServiceServices _homeServiceServices;
        private readonly IBaseEntitiesServices _baseEntitiesServices;
        private readonly IMemoryCache _memoryCache;
        public HomeServiceAppServices(IHomeServiceServices homeServiceServices, IBaseEntitiesServices baseEntitiesServices, IMemoryCache memoryCache)
        {
            _homeServiceServices = homeServiceServices;
            _baseEntitiesServices = baseEntitiesServices;
            _memoryCache = memoryCache;
        }

        public async Task<Result> CreateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            if (homeService.ProfileImgFile is not null)
            {
                homeService.ImagePath = await _baseEntitiesServices.UploadImage(homeService.ProfileImgFile!, "Homeservice", cancellationToken);
            }
            if (await _homeServiceServices.GetTitleHomeService(homeService.Title, cancellationToken))
            {
                return new Result(false, "خدمات موجود می باشد.");

            }
          
            else
            {
                var homeservice1 = new HomeService
                {
                    Id=homeService.Id,
                    Title = homeService.Title,
                    BasePrice = homeService.BasePrice,
                    Description = homeService.Description,
                   SubCategoryId=homeService.SubCategoryId,
                    ImagePath = homeService.ImagePath,
                   
                    
                  
                    

                };
              
                   
                await _homeServiceServices.CreateHomeService(homeService, cancellationToken);
                return new Result(true, "خدمات با موفقیت اضافه شد.");
            }
        }

        public async Task<Result> DeleteHomeService(int id, CancellationToken cancellationToken)
        {
            if (await _homeServiceServices.GetHomeServiceById(id, cancellationToken) != null)
            {
                await _homeServiceServices.DeleteHomeService(id, cancellationToken);
                return new Result(true, "با موفقیت حذف شد.");
            }
            else
                return new Result(false, " همچین خدماتی وجود ندارد.");
        }

        public async Task<List<HomeServiceDto>> GetAllHomeService(CancellationToken cancellationToken)
        {
            List<HomeServiceDto> homeServices;
            
            if (_memoryCache.Get("HomeServiceList") is not null)
            {
                homeServices = _memoryCache.Get<List<HomeServiceDto>>("HomeServiceList");
               
            }
            else
            {
                homeServices = await _homeServiceServices.GetAllHomeService(cancellationToken);
                _memoryCache.Set("HomeServiceList", homeServices, TimeSpan.FromHours(2));
            }




            return homeServices;
            

           
        }

        public async Task<HomeService> GetHomeServiceById(int id, CancellationToken cancellationToken)
        {
            if (await _homeServiceServices.GetHomeServiceById(id, cancellationToken) != null)
            {
                return await _homeServiceServices.GetHomeServiceById(id, cancellationToken);

            }
            else
                return null;
        }

        public async Task<List<HomeServiceDto>> GetHomeServicesBySubCategoryId(int subCategoryId, CancellationToken cancellationToken)
        {
            return await _homeServiceServices.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);
        }

        public async Task<Result> UpdateExpertServices(int expertId, List<int> selectedHomeServices, CancellationToken cancellationToken)
        {
            await _homeServiceServices.UpdateExpertServices(expertId, selectedHomeServices, cancellationToken);
            return new Result(true, "ویرایش با موفقیت انجام شد.");
        }

        public async Task<Result> UpdateHomeService(HomeServiceDto homeService, CancellationToken cancellationToken)
        {
            if (homeService.ProfileImgFile is not null)
            {
                homeService.ImagePath = await _baseEntitiesServices.UploadImage(homeService.ProfileImgFile!, "Homeservice", cancellationToken);
            }
           
             await _homeServiceServices.UpdateHomeService(homeService, cancellationToken);
                return new Result(true, "ویرایش با موفقیت انجام شد.");
            
        }
    }
}
