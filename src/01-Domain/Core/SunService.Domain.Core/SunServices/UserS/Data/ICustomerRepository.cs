using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.Data
{
    public interface ICustomerRepository
    {
        public Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken);
        public Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task CreateCustomer(Customer customer, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task DeleteCustomer(int id, CancellationToken cancellationToken);
        public global::System.Threading.Tasks.Task UpdateCustomer(Customer customer, CancellationToken cancellationToken);
    }
}
