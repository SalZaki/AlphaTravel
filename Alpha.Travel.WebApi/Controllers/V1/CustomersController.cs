﻿namespace Alpha.Travel.WebApi.Controllers.V1
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
    using Alpha.Travel.Application.Customers.Commands;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Produces("application/xml", "application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string _documentationUrl;

        public CustomersController(
            IMediator mediator,
            IMapper mapper,
            IOptionsSnapshot<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
            _mediator = mediator;
            _mapper = mapper;
            _documentationUrl = _apiSettings.ApiDocumentationUrl.Replace("{VERSION}", "1") + "/customers";
        }

        /// <summary>
        /// Retrieve the customer by id.
        /// </summary>
        /// <param name="id">The Id of customer.</param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A destination.</returns>
        [HttpGet("{id}", Name = nameof(GetCustomerByIdAsync))]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerByIdAsync(
            [FromRoute] string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = new GetCustomerPreviewQuery { Id = id };
            var customer = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var response = _mapper.Map<Customer>(customer);
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
        [ProducesResponseType(typeof(PagedCollection<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomersAsync(
            [FromQuery]PagingOptions pagingOptions,
            [FromQuery]SortOptions sortOptions,
            [FromQuery]SearchOptions searchOptions,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            pagingOptions.Limit = pagingOptions.Limit ?? _apiSettings.DefaultPageSize;
            pagingOptions.Offset = pagingOptions.Offset ?? _apiSettings.DefaultPageIndex;

            var query = new GetCustomersPreviewQuery
            {
                Offset = pagingOptions.Offset.Value,
                Limit = pagingOptions.Limit.Value,
                OrderBy = sortOptions.OrderBy,
                Query = searchOptions.Query
            };

            var customers = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var items = _mapper.Map<Customer[]>(customers.Items);

            var response = PagedCollection<Customer>.Create(
                Link.ToCollection(nameof(GetAllCustomersAsync)),
                items,
                customers.Count,
                pagingOptions);

            return Ok(response);
        }

        /// <summary>
        /// Add a customer
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(AddCustomerAsync))]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomerAsync(
            [FromBody]CreateCustomer command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _mediator.Send(command);
            return Created(Url.Link(nameof(GetCustomerByIdAsync), new { result.Id }), null);
        }

        /// <summary>
        /// Deteles a specified customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteCustomerByIdAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerByIdAsync(
            [FromRoute]string id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteCustomer { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates a customer
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateCustomerAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomerAsync(
            [FromRoute]string id,
            [FromBody]UpdateCustomer command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}