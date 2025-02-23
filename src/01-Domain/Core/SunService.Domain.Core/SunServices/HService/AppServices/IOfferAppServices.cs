using SunService.Domain.Core.SunServices.HService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.HService.AppServices
{
    public interface IOfferAppServices
    {
        public Task<List<OfferDto>> GetAllOffer(int OrderId, CancellationToken cancellationToken);
    }
}
