namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Application.Destinations.Models;
    using Application.Destinations.Queries;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class DestinationController : Controller
    {
        private readonly IMediator _mediator;

        public DestinationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DestinationPreviewDto))]
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

        [HttpGet("getall", Name = nameof(GetAllAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DestinationPreviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetDestinationsPreviewQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (query == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}