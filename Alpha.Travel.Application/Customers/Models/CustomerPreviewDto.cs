namespace Alpha.Travel.Application.Models
{
    using System;
    using System.Linq.Expressions;
    using Domain.Entities;
    using Newtonsoft.Json;

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

        [JsonProperty(Order = -10, PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(Order = -9, PropertyName = "first_name")]
        public string Firstname { get; set; }

        [JsonProperty(Order = -8, PropertyName = "Surname")]
        public string Surname { get; set; }

        [JsonProperty(Order = -7, PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(Order = -6, PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(Order = -5, PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(Order = -4, PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(Order = -3, PropertyName = "modified_on", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(Order = -2, PropertyName = "modified_by", NullValueHandling = NullValueHandling.Ignore)]
        public string ModifiedBy { get; set; }
    }
}