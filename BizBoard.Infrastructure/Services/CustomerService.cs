using BizBoard.Application.DTOs;
using BizBoard.Application.Interfaces;
using BizBoard.Domain.Entities;
using BizBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BizBoard.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BizBoardDbContext _context;

        public CustomerService(BizBoardDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return await _context.Customer
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FirstName = c.Name,
                    LastName = c.Surname,
                    Email = c.Email
                })
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.Name,
                LastName = customer.Surname,
                Email = customer.Email
            };
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
        {
            var entity = new Customer
            {
                Name = input.FirstName,
                Surname = input.LastName,
                Email = input.Email
            };

            _context.Customer.Add(entity);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = entity.Id,
                FirstName = entity.Name,
                LastName = entity.Surname,
                Email = entity.Email
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null) return false;

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
