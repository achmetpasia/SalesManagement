using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SalesManagement.API.Controllers
{
    /// <summary>
    /// Base controller providing access to the Mediator instance for handling commands and queries.
    /// </summary>
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Constructor for BaseController.
        /// </summary>
        /// <param name="mediator">The Mediator instance.</param>
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the Mediator instance for handling commands and queries.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
