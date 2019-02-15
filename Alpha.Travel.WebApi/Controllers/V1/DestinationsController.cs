namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Application.Categories.Models;
    using Application.Destinations.Queries;

    public class DestinationsController : BaseController
    {
        [HttpGet]
        [HttpGet("{destinationId:int}", Name = nameof(GetDestinationByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DestinationPreviewDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDestinationByIdAsync([FromQuery] GetDestinationPreviewQuery query, CancellationToken ct)
        {
            if (query.DestinationId <= 0) return BadRequest();
            return Ok(await Mediator.Send(query, ct));
        }
    }
}