namespace Alpha.Travel.WebApi.Controllers.V1
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        protected IMediator Mediator { get; }

        public BaseController()
        {
            Mediator = HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}