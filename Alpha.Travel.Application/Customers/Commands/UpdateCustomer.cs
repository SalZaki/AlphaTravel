namespace Alpha.Travel.Application.Customers.Commands
{
    using MediatR;
    using System;

    public class UpdateCustomer : IRequest
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}