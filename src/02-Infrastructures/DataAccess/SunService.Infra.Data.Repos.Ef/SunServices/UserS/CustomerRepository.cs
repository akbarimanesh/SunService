using Microsoft.EntityFrameworkCore;
using SunService.Domain.Core.SunServices.HService.DTOs;
using SunService.Domain.Core.SunServices.HService.Entities;
using SunService.Domain.Core.SunServices.UserS.Data;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;
using SunService.Infra.Data.Db.SqlServer.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Infra.Data.Repos.Ef.SunServices.UserS
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            await _appDbContext.Customers.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var customer = await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken)
        {
            return await _appDbContext.Customers.AsNoTracking().Select(x => new CustomerDto()
            {

                Id = x.Id,
                CustomerFullName = x.FirstName + " " + x.LastName,
                Address=x.Address,
                City=x.City.Title,
                Email=x.Email,
                Mobile=x.Mobile,
              

            }).ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            var customer1 = await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id, cancellationToken);
            customer1.Id = customer.Id;
            customer1.FirstName = customer.FirstName;
            customer1.LastName= customer.LastName;
            customer1.ShabaNumber= customer.ShabaNumber;
            customer1.CardNumber = customer.CardNumber;
            customer1.Address = customer.Address;
            customer1.Balance= customer.Balance;
            customer1.City= customer.City;
            customer1.Email= customer.Email;
            customer1.UserName= customer.UserName;
            customer1.PasswordHash= customer.PasswordHash;
            customer1.Mobile= customer.Mobile;
           
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
