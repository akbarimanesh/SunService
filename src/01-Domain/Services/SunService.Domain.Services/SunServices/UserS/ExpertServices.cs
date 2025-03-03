using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;

namespace SunService.Domain.Services.SunServices.UserS
{
    public class ExpertServices : IExpertServices
    {
        private readonly IExpertRepository _ExpertRepository;

        public ExpertServices(IExpertRepository expertRepository)
        {
            _ExpertRepository = expertRepository;
        }

        public async Task CreateExpert(Expert expert, CancellationToken cancellationToken)
        {
            await _ExpertRepository.CreateExpert(expert, cancellationToken);
        }

        public async Task DeleteExpert(int id, CancellationToken cancellationToken)
        {
            await _ExpertRepository.DeleteExpert(id, cancellationToken);
        }

        public async Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken)
        {
            return await _ExpertRepository.GetAllExperts(cancellationToken);
        }

        public async Task<Expert> GetExpertById(int homeServiceId, CancellationToken cancellationToken)
        {
            return await _ExpertRepository.GetExpertById(homeServiceId, cancellationToken);
        }

        public async Task UpdateExpert(Expert expert, CancellationToken cancellationToken)
        {
            await _ExpertRepository.UpdateExpert(expert, cancellationToken);
        }
    }
}
