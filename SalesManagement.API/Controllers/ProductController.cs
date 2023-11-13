using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Models.Products;

namespace SalesManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing product operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="command">The command to create a product.</param>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/Product
        /// {
        ///     "Name": "Sample Product",
        ///     "Price": 19.99
        /// }
        /// </remarks>
        /// <returns>Returns the result of the product creation operation.</returns>
        /// <response code="200">Returns the result of the product creation operation.</response>
        /// <response code="400">If the request is invalid or the product creation fails.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [ProducesResponseType(typeof(ObjectBaseResponse<CreateProductResponse>), 201)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await Mediator.Send(command);

            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="request">The request containing updated product information.</param>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /api/Product/{id}
        /// {
        ///     "Name": "Updated Product Name",
        ///     "Price": 29.99
        /// }
        /// </remarks>
        /// <returns>Returns the updated product information.</returns>
        /// <response code="200">Returns the updated product information.</response>
        /// <response code="400">If the request is invalid or the product update fails.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [ProducesResponseType(typeof(ObjectBaseResponse<UpdateProductResponse>), 200)]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
        {
            var result = await Mediator.Send(
                new UpdateProductCommand(
                id,
                request.Name,
                request.Price));

            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <response code="204">Returns NoContent if the product is successfully deleted.</response>
        /// <response code="400">If the request is invalid or the product deletion fails.</response>
        /// <response code="500">If an error occurred while processing the request.</response>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}
