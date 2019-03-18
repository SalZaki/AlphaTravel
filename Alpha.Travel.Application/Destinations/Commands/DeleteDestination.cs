namespace Alpha.Travel.Application.Destinations.Commands
{
    using MediatR;

    public class DeleteDestination : IRequest
    {
        public string Id { get; set; }
    }
}