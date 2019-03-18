namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Application.Destinations.Commands;
    using Application.Destinations.Queries;

    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class DestinationsController : ControllerBase
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string _documentationUrl;

        public DestinationsController(
            IMediator mediator,
            IMapper mapper,
            IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
            _mediator = mediator;
            _mapper = mapper;
            _documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", "1") + "/destinations";
        }

        /// <summary>
        /// Retrieve the destination by id.
        /// </summary>
        /// <param name="id">The Id of destination.</param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A destination.</returns>
        [HttpGet("{id}", Name = nameof(GetByIdAsync))]
        [ProducesResponseType(typeof(Destination), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = new GetDestinationPreviewQuery { Id = id };
            var destination = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var response = _mapper.Map<Destination>(destination);
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
        [HttpGet(Name = nameof(GetAllAsync))]
        [ProducesResponseType(typeof(PagedCollection<Destination>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery]PagingOptions pagingOptions,
            [FromQuery]SortOptions sortOptions,
            [FromQuery]SearchOptions searchOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            pagingOptions.Limit = pagingOptions.Limit ?? _apiSettings.DefaultPageSize;
            pagingOptions.Offset = pagingOptions.Offset ?? _apiSettings.DefaultPageIndex;

            var query = new GetDestinationsPreviewQuery
            {
                Offset = pagingOptions.Offset.Value,
                Limit = pagingOptions.Limit.Value,
                OrderBy = sortOptions.OrderBy,
                Query = searchOptions.Query
            };

            var destinations = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var items = _mapper.Map<Destination[]>(destinations.Items);

            var response = PagedCollection<Destination>.Create(
                Link.ToCollection(nameof(GetAllAsync)),
                items,
                destinations.Count,
                pagingOptions);

            return Ok(response);
        }

        /// <summary>
        /// Addes a destination
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(AddAsync))]
        [ProducesResponseType(typeof(Destination), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync(
            [FromBody]CreateDestination command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Created(Url.Link(nameof(GetByIdAsync), new { result.Id }), null);
        }

        /// <summary>
        /// Deteles a specified destination
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute]string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteDestination { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary>
        /// Updates a destionation
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute]string id,
            [FromBody]UpdateDestination command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}