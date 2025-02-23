using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.UserS
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ActiveUser(int id, CancellationToken cToken)
        {
            await _userRepository.ActiveUser(id, cToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _userRepository.Delete(id, cancellationToken);
        }

        public async Task<List<UserSummaryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _userRepository.GetAll(cancellationToken);
        }

        public async Task<UserDto> GetById(int id, CancellationToken cancellationToken)
        {
           return await _userRepository.GetById(id, cancellationToken);
        }

        public async Task<int> GetCount(CancellationToken cancellationToken)
        {
            return await _userRepository.GetCount(cancellationToken);
        }

        public async Task SaveImageUser(int id, string imagepath, CancellationToken cancellationToken)
        {
            await _userRepository.SaveImageUser(id, imagepath, cancellationToken);  
        }

        public async Task<bool> StatusUser(string username, CancellationToken cancellationToken)
        {
            return  await _userRepository.StatusUser(username, cancellationToken);
        }

        public async Task<bool> Update(UserDto model, CancellationToken cancellationToken)
        {
            return await _userRepository.Update(model, cancellationToken);
        }
    }
}
