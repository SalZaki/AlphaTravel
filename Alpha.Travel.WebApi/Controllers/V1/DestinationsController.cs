namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Application.Categories.Models;
    using Application.Destinations.Queries;

    public class DestinationsController : Controller
    {
        private readonly IMediator _mediator;

        public DestinationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HttpGet("{destinationId:int}", Name = nameof(GetDestinationByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DestinationPreviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDestinationByIdAsync([FromQuery] GetDestinationPreviewQuery query, CancellationToken ct)
        {
            if (query.DestinationId <= 0) return BadRequest();
            return Ok(await _mediator.Send(query, ct));
        }
    }
}