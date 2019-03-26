namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Application.Destinations.Commands;
    using Application.Destinations.Queries;
    using Application.Models;
    using Application.Common.Models;

    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class DestinationsController : ControllerBase
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMediator _mediator;
        private readonly string _documentationUrl;

        public DestinationsController(
            IMediator mediator,
            IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
            _mediator = mediator;
            _documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", "1") + "/destinations";
        }

        /// <summary>
        /// Retrieve the destination by id.
        /// </summary>
        /// <param name="id">The Id of destination.</param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A destination.</returns>
        [HttpGet("{id}", Name = nameof(GetDestinationByIdAsync))]
        [ProducesResponseType(typeof(DestinationPreviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDestinationByIdAsync(
            [FromRoute] string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = new GetDestinationPreviewQuery { Id = id };
            var response = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Returns a paged collection for destinations.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="sortOptions"></param>
        /// <param name="searchOptions"></param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A paged collection of destinations.</returns>
        [HttpGet(Name = nameof(GetAllDestinationsAsync))]
        [ProducesResponseType(typeof(PagedResult<DestinationPreviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDestinationsAsync(
            [FromQuery]PagingOptions pagingOptions,
            [FromQuery]SortOptions sortOptions,
            [FromQuery]SearchOptions searchOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            pagingOptions.PageNumber = pagingOptions.PageNumber ?? _apiSettings.DefaultPageNumber;
            pagingOptions.PageSize = pagingOptions.PageSize ?? _apiSettings.DefaultPageSize;

            var query = new GetDestinationsPreviewQuery
            {
                PageNumber = pagingOptions.PageNumber.Value,
                PageSize = pagingOptions.PageSize.Value,
                OrderBy = sortOptions.OrderBy,
                Query = searchOptions.Query
            };

            var destinations = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

            return Ok(destinations);
        }

        /// <summary>
        /// Adds a destination
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(AddDestinationAsync))]
        [ProducesResponseType(typeof(DestinationPreviewDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDestinationAsync(
            [FromBody]CreateDestination command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Created(Url.Link(nameof(GetDestinationByIdAsync), new { result.Id }), null);
        }

        /// <summary>
        /// Deteles a specified destination
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteDestinationAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDestinationAsync(
            [FromRoute]string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteDestination { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary>
        /// Updates a destination
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateDestinationAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDestinationAsync(
            [FromRoute]string id,
            [FromBody]UpdateDestination command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}