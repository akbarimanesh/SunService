using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Services
{
    public interface IExpertServices
    {
        public Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken);
        public Task<Expert> GetExpertById(int homeServiceId, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateExpert(Expert expert, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteExpert(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateExpert(Expert expert, CancellationToken cancellationToken);
    }
}
