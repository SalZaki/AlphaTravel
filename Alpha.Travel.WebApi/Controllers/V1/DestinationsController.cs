namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Application.Destinations.Models;
    using Application.Destinations.Queries;
    using Application.Common.Models.Requests;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class DestinationsController : Controller
    {
        private readonly IMediator _mediator;

        public DestinationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DestinationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var query = new GetDestinationPreviewQuery { Id = id };
            var response = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

            if (response == null)
            {
                return new NotFoundObjectResult($"The destination with id {query.Id} you were requesting could not be found");
            }

            return Ok(response);
        }

        [HttpGet(Name = nameof(GetAllAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDestinationResponse))]
        // [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingOptions pagingOptions, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (pagingOptions == null)
            {
                return BadRequest();
            }

            var query = new GetDestinationsPreviewQuery
            {
                PageNumber = pagingOptions.PageNumber,
                PageSize = pagingOptions.PageSize == 0 ? 10 : pagingOptions.PageSize,
                OrderBy = pagingOptions.OrderBy,
                Sort = pagingOptions.Sort
            };

            var response = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return Ok(response);
        }
    }
}