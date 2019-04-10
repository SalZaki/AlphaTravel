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
    using Application.Customers.Queries;
    using Application.Customers.Commands;
    using System.Collections.Generic;

    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IResponseFactory _responseFactory;
        private readonly string _documentationUrl;

        public CustomersController(
            IMediator mediator,
            IMapper mapper,
            IResponseFactory responseFactory,
            IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
            _mediator = mediator;
            _mapper = mapper;
            _responseFactory = responseFactory;
            _documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", "1") + "/customers";
        }

        /// <summary>
        /// Retrieve the customer by id.
        /// </summary>
        /// <param name="id">The Id of customer.</param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A destination.</returns>
        [HttpGet("{id:int}", Name = nameof(GetCustomerByIdAsync))]
        [ProducesResponseType(typeof(Response<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerByIdAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = new GetCustomerPreviewQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var customer = _mapper.Map<Customer>(result);
            var response = _responseFactory.CreateReponse(customer, "Success", "1.0.0");
            return Ok(response);
        }

        /// <summary>
        /// Returns a paged collection for customers.
        /// </summary>
        /// <param name="pagingOptions"></param>
        /// <param name="sortOptions"></param>
        /// <param name="searchOptions"></param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A paged collection of customers.</returns>
        [HttpGet(Name = nameof(GetAllCustomersAsync))]
        [ProducesResponseType(typeof(PagedResponse<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomersAsync(
            [FromQuery]PagingOptions pagingOptions,
            [FromQuery]SortOptions sortOptions,
            [FromQuery]SearchOptions searchOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            pagingOptions.PageNumber = pagingOptions.PageNumber ?? _apiSettings.DefaultPageNumber;
            pagingOptions.PageSize = pagingOptions.PageSize ?? _apiSettings.DefaultPageSize;

            var query = new GetCustomersPreviewQuery
            {
                PageNumber = pagingOptions.PageNumber.Value,
                PageSize = pagingOptions.PageSize.Value,
                OrderBy = sortOptions.OrderBy,
                Query = searchOptions.Query
            };

            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var customers = _mapper.Map<IList<Customer>>(result);
            var response = _responseFactory.CreatePagedReponse(customers, query, "Success", "1.0.0");
            return Ok(response);
        }

        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(AddCustomerAsync))]
        [ProducesResponseType(typeof(Response<Customer>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomerAsync(
            [FromBody]CreateCustomer command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Created(Url.Link(nameof(GetCustomerByIdAsync), new { result.Id }), null);
        }

        /// <summary>
        /// Deteles a specified customer
        /// </summary>
        /// <param name="id">customer id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = nameof(DeleteCustomerByIdAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerByIdAsync(
            [FromRoute]int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteCustomer { Id = id };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary>
        /// Updates a customer
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id:int}", Name = nameof(UpdateCustomerAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomerAsync(
            [FromRoute]int id,
            [FromBody]UpdateCustomer command,
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