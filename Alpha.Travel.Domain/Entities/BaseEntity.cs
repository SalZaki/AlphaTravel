namespace Alpha.Travel.Domain.Entities
{
    using System;

    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
    }
}