namespace Alpha.Travel.Application.Customers.Models
{
    using Common.Models;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class CustomerPreviewDto: BaseDto
    {
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}