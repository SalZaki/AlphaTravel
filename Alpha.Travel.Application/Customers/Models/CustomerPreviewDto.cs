namespace Alpha.Travel.Application.Models
{
    using Domain.Entities;
    using System;
    using System.Linq.Expressions;

    public sealed class CustomerPreviewDto
    {
        public CustomerPreviewDto() { }

        public static Expression<Func<Customer, CustomerPreviewDto>> Projection
        {
            get
            {
                return c => new CustomerPreviewDto
                {
                    Id = c.Id,
                    Firstname = c.Firstname,
                    Surname = c.Surname,
                    Email = c.Email,
                    Password = c.Password,
                    CreatedBy = c.CreatedBy,
                    CreatedOn = c.CreatedOn,
                    ModifiedBy = c.ModifiedBy,
                    ModifiedOn = c.ModifiedOn
                };
            }
        }

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}