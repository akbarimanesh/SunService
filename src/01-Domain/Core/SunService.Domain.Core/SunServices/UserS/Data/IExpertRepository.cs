using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Data
{
    public interface IExpertRepository
    {
        public Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken);
        public Task<Expert> GetCustomerById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateExpert(Expert expert, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteExpert(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateExpert(Expert expert, CancellationToken cancellationToken);
    }
}
