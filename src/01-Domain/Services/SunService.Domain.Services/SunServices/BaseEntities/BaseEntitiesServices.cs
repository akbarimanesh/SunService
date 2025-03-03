using Microsoft.AspNetCore.Http;
using SunService.Domain.Core.SunServices.BaseEntities.Data;
using SunService.Domain.Core.SunServices.BaseEntities.Services;
using SunService.Domain.Core.SunServices.HService.Entities;
using System.Net.Http.Headers;

namespace SunService.Domain.Services.SunServices.BaseEntities
{
    public class BaseEntitiesServices : IBaseEntitiesServices
    {
        private readonly IBaseEntitiesRepository _BaseEntitiesRepository;
        public BaseEntitiesServices(IBaseEntitiesRepository baseEntitiesRepository)
        {
            _BaseEntitiesRepository = baseEntitiesRepository;
        }

        public async Task AddOrderImages(List<string> imgAddress, int orderId, CancellationToken cancellationToken)
        {
            await _BaseEntitiesRepository.AddOrderImages(imgAddress, orderId, cancellationToken);
        }

        public async Task CreateCity(City city, CancellationToken cancellationToken)
        {
            await _BaseEntitiesRepository.CreateCity(city, cancellationToken);
        }

        public async Task DeleteCity(int id, CancellationToken cancellationToken)
        {
            await _BaseEntitiesRepository.DeleteCity(id, cancellationToken);
        }

        public async Task<List<City>> GetCities(CancellationToken cancellationToken)
        {
            return await _BaseEntitiesRepository.GetCities(cancellationToken);
        }

        public async Task<City> GetCityById(int id, CancellationToken cancellationToken)
        {
            return await _BaseEntitiesRepository.GetCityById(id, cancellationToken);
        }

        public async Task UpdateCity(City city, CancellationToken cancellationToken)
        {
             await _BaseEntitiesRepository.UpdateCity(city, cancellationToken);
        }

        public async Task<string> UploadImage(IFormFile FormFile, string folderName, CancellationToken cancellationToken)
        {
            string filePath;
            string fileName;
            if (FormFile != null)
            {
                fileName = Guid.NewGuid().ToString() +
                           ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
               //  filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Category");
                filePath = Path.Combine("wwwroot", "Images", folderName, fileName);

                try
                {
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FormFile.CopyToAsync(stream, cancellationToken);
                    }
                }
                catch
                {
                    throw new Exception("Upload files operation failed");
                }
                return $"/Images/{folderName}/{fileName}";
            }
            else
                fileName = "";

            return fileName;
        }
    }
}
