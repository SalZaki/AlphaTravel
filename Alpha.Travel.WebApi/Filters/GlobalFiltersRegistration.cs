namespace Alpha.Travel.WebApi.Filters
{
    using Exceptions;
    using Exceptions.Destinations;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            filterCollection.Add<DestinationNotFoundExceptionFilter>();
            filterCollection.Add<CustomerNotFoundExceptionFilter>();
            filterCollection.Add<ValidationExceptionFilter>();
        }

        public static void AddGlobalCustomFilters(this FilterCollection filterCollection) { }
    }
}