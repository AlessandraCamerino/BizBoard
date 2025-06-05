using BizBoard.Application.DTOs;
using BizBoard.Infrastructure.Persistence;
using BizBoard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BizBoard.Tests.Services
{
    public class CustomerServiceTests
    {
        private BizBoardDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BizBoardDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new BizBoardDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_New_Customer()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new CustomerService(context);

            var input = new CreateCustomerDto
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com"
            };

            // Act
            var result = await service.CreateAsync(input);
            var customers = await service.GetAllAsync();

            // Assert
            Assert.Single(customers);
            Assert.Equal("Test", result.FirstName);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Empty_When_No_Customers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new CustomerService(context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
