namespace Alpha.Travel.Application.Common.Models
{
    using System;

    public interface IDto
    {
        int Id { get; set; }

        DateTime CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime ModifiedOn { get; set; }

        string ModifiedBy { get; set; }
    }
}