namespace Alpha.Travel.Application.Customers.Queries
{
    using System;
    using System.Linq;

    public static class GetCustomersPreviewQueryExtensions
    {
        public static bool HasPrevious(this GetCustomersPreviewQuery queryParameters)
        {
            return (queryParameters.Limit > 1);
        }

        public static bool HasNext(this GetCustomersPreviewQuery queryParameters, int totalCount)
        {
            return (queryParameters.Limit < (int)GetTotalPages(queryParameters, totalCount));
        }

        public static double GetTotalPages(this GetCustomersPreviewQuery queryParameters, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)queryParameters.Limit);
        }

        public static bool HasQuery(this GetCustomersPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.Query);
        }

        public static bool HasOrder(this GetCustomersPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.OrderBy);
        }

        public static bool IsDescending(this GetCustomersPreviewQuery queryParameters)
        {
            if (!string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                return queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            }
            return false;
        }
    }
}