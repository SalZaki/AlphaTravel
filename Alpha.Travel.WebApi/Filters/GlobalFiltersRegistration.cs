namespace Alpha.Travel.WebApi.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Diagnostics.CodeAnalysis;

    using Exceptions;
    using Exceptions.Destination;
    using Exceptions.Customer;

    [ExcludeFromCodeCoverage]
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            filterCollection.Add<DestinationNotFoundExceptionFilter>();
            filterCollection.Add<CustomerNotFoundExceptionFilter>();
            filterCollection.Add<ValidationExceptionFilter>();
        }
    }
}