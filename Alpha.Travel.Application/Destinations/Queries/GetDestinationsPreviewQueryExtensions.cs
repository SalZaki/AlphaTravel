namespace Alpha.Travel.Application.Destinations.Queries
{
    using System;
    using System.Linq;

    public static class GetDestinationsPreviewQueryExtensions
    {
        public static bool HasPrevious(this GetDestinationsPreviewQuery queryParameters)
        {
            return (queryParameters.Limit > 1);
        }

        public static bool HasNext(this GetDestinationsPreviewQuery queryParameters, int totalCount)
        {
            return (queryParameters.Limit < (int)GetTotalPages(queryParameters, totalCount));
        }

        public static double GetTotalPages(this GetDestinationsPreviewQuery queryParameters, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)queryParameters.Limit);
        }

        public static bool HasQuery(this GetDestinationsPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.Query);
        }

        public static bool HasOrder(this GetDestinationsPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.OrderBy);
        }

        public static bool IsDescending(this GetDestinationsPreviewQuery queryParameters)
        {
            if (!string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                return queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            }
            return false;
        }
    }
}