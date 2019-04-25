namespace Alpha.Travel.WebApi.Controllers.V1
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;
    using Models;
    using AutoMapper;
    using Application.Customers.Queries;
    using Application.Customers.Commands;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Routing;

    [Produces("application/xml", "application/json")]
    public abstract class BaseController : ControllerBase
    {
        public ApiSettings ApiSettings { get; }

        public IMediator Mediator { get; }

        public IMapper Mapper { get; }

        public IResponseFactory ResponseFactory { get; }

        public string DocumentationUrl { get; set; }

        public BaseController(IMediator mediator,
            IMapper mapper,
            IResponseFactory responseFactory,
            IOptionsSnapshot<ApiSettings> apiSettings)
        {
            ApiSettings = apiSettings.Value;
            Mediator = mediator;
            Mapper = mapper;
            ResponseFactory = responseFactory;
        }
    }

    //[Authorize]
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class CustomersController : BaseController
    {
        public CustomersController(IMediator mediator, IMapper mapper, IResponseFactory responseFactory, IOptionsSnapshot<ApiSettings> apiSettings) :
            base(mediator, mapper, responseFactory, apiSettings)
        {
            DocumentationUrl = ApiSettings.ApiDocumentationUrl.Replace("{VERSION}", "1") + "/customers";
        }

        /// <summary>
        /// Gets customer by id.
        /// </summary>
        /// <param name="id">The Id of customer.</param>
        /// <param name="cancellationToken">a cancellation toekn.</param>
        /// <returns>A destination.</returns>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetCustomerByIdAsync))]
        [ProducesResponseType(typeof(Response<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerByIdAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = new GetCustomerPreviewQuery { Id = id };
            var result = await Mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var customer = Mapper.Map<Customer>(result);

            var response = ResponseFactory.CreateReponse(customer, typeof(CustomersController), ResponseStatus.Success, "1.0.0");
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
        [HttpGet]
        [Route("", Name = nameof(GetAllCustomersAsync))]
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
            pagingOptions.PageNumber = pagingOptions.PageNumber ?? ApiSettings.DefaultPageNumber;
            pagingOptions.PageSize = pagingOptions.PageSize ?? ApiSettings.DefaultPageSize;

            var query = new GetCustomersPreviewQuery
            {
                PageNumber = pagingOptions.PageNumber.Value,
                PageSize = pagingOptions.PageSize.Value,
                OrderBy = sortOptions.OrderBy,
                Query = searchOptions.Query
            };

            var result = await Mediator.Send(query, cancellationToken).ConfigureAwait(false);
            var customers = Mapper.Map<List<Customer>>(result);
            var response = ResponseFactory.CreatePagedReponse(customers, typeof(CustomersController), query, ResponseStatus.Success, "1.0.0");
            return Ok(response);
        }

        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = nameof(AddCustomerAsync))]
        [ProducesResponseType(typeof(Response<Customer>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCustomerAsync(
            [FromBody]CreateCustomer command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            var customer = Mapper.Map<Customer>(result);
            return Created(Url.Link(nameof(GetCustomerByIdAsync), new { customer.Id }), ResponseFactory.CreateReponse(customer, typeof(CustomersController), ResponseStatus.Success, "1.0.0"));
        }

        /// <summary>
        /// Deteles a specified customer
        /// </summary>
        /// <param name="id">customer id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}", Name = nameof(DeleteCustomerByIdAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerByIdAsync(
            [FromRoute]int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteCustomer { Id = id };
            await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateCustomerAsync))]
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
            await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}