using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Models.Customers;

namespace SalesManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing customer operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/customers
        ///     {
        ///         "firstName": "John",
        ///         "lastName": "Doe",
        ///         "address": "123 Main St",
        ///         "postalCode": "12345"
        ///     }
        /// </remarks>
        /// <param name="command">The command to create a new customer.</param>
        /// <returns>
        /// An <see cref="ObjectBaseResponse{T}"/> containing information about the result of the operation.
        /// </returns>
        /// <response code="201">Returns the newly created customer.</response>
        /// <response code="400">If the request is invalid or the command is null.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [ProducesResponseType(typeof(ObjectBaseResponse<CreateCustomerResponse>), 201)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);

            return StatusCode((int) result.StatusCode, result);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to update.</param>
        /// <param name="request">The request payload containing updated customer information.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/customers/{id}
        ///     {
        ///         "firstName": "UpdatedFirstName",
        ///         "lastName": "UpdatedLastName",
        ///         "address": "UpdatedAddress",
        ///         "postalCode": "UpdatedPostalCode"
        ///     }
        /// </remarks>
        /// <returns>
        /// An <see cref="ObjectBaseResponse{T}"/> containing information about the result of the operation.
        /// </returns>
        /// <response code="200">Returns the updated customer.</response>
        /// <response code="400">If the request is invalid or the customer with the given ID is not found.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [ProducesResponseType(typeof(ObjectBaseResponse<UpdateCustomerResponse>), 200)]
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

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>
        /// No content if the deletion is successful.
        /// </returns>
        /// <response code="204">Returns no content if the deletion is successful.</response>
        /// <response code="404">If the customer with the given ID is not found.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCustomerCommand(id));

            return NoContent();
        }
    }
}
