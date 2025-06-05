using BizBoard.Application.DTOs;

namespace BizBoard.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto input);
        Task<bool> DeleteAsync(int id);
    }
}
