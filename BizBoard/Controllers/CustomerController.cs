using BizBoard.Application.DTOs;
using BizBoard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BizBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto input)
        {
            var created = await _customerService.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
