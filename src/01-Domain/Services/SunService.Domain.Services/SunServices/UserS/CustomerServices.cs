using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Domain.Core.SunServices.UserS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Services.SunServices.UserS
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        public async Task CreateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            await _CustomerRepository.CreateCustomer(customer, cancellationToken);
        }

        public async Task DeleteCustomer(int id, CancellationToken cancellationToken)
        {
           await _CustomerRepository.DeleteCustomer(id, cancellationToken);
        }

        public async Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken)
        {
            return await _CustomerRepository.GetAllCustomers(cancellationToken);
        }

        public async Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken)
        {
            return await _CustomerRepository.GetCustomerById(id, cancellationToken);
        }

        public async Task UpdateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            await _CustomerRepository.UpdateCustomer(customer,cancellationToken);
        }
    }
}
