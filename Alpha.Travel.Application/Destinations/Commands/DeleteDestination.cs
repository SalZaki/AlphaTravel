namespace Alpha.Travel.Application.Destinations.Commands
{
    using MediatR;

    public class DeleteDestination : IRequest
    {
        public int Id { get; set; }
    }
}