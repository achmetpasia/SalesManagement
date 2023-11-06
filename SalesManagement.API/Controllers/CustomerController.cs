using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Models.Customers;

namespace SalesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);

            return StatusCode((int) result.StatusCode, result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
        {
            var result = await Mediator.Send(
                new UpdateCustomerCommand(
                id,
                request.FirstName,
                request.LastName,
                request.Address,
                request.PostalCode));

            return StatusCode((int)result.StatusCode, request);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCustomerCommand(id));

            return NoContent();
        }
    }
}
