namespace Alpha.Travel.Application.Customers.Commands
{
    using MediatR;

    public class DeleteCustomer : IRequest
    {
        public string Id { get; set; }
    }
}