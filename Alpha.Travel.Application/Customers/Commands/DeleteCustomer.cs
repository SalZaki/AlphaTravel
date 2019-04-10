namespace Alpha.Travel.Application.Customers.Commands
{
    using MediatR;

    public class DeleteCustomer : IRequest
    {
        public int Id { get; set; }
    }
}