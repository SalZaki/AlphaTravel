namespace Alpha.Travel.Domain.Entities
{
    using System;

    public sealed class ApiClient : BaseEntity
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }
    }
}