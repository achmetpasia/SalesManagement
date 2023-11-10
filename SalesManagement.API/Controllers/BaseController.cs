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
        /// Gets the Mediator instance for handling commands and queries.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
