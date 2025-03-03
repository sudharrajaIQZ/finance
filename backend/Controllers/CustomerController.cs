using backend.Dto;
using backend.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerIntrerface customerIntrerface;
        public CustomerController(ICustomerIntrerface Icustomer)
        {
            this.customerIntrerface = Icustomer;
        }
        [HttpGet]
        public async Task<IActionResult> getCustomers()
        {
            var customers = await customerIntrerface.getCustomerAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var customers = await customerIntrerface.createCustomerAsync(customer);
            return StatusCode(customers.Code,customers);
        }

        [HttpPost]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> updateCustomer([FromRoute] Guid id, UpdateCustomer updateCustomer)
        {
            var customers = await customerIntrerface.updateCustomerAsync(id, updateCustomer);
            return StatusCode(customers.Code,customers);
        }

        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> findCustomer([FromRoute] string id)
        {
            var findCustomer = await customerIntrerface.findCustomerAsync(id);
            return StatusCode(findCustomer.Code, findCustomer);
        }
    }
}
