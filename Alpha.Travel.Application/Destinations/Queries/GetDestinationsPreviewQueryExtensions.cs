namespace Alpha.Travel.Application.Destinations.Queries
{
    using System;
    using System.Linq;

    public static class GetDestinationsPreviewQueryExtensions
    {
        public static bool HasPrevious(this GetDestinationsPreviewQuery queryParameters)
        {
            return (queryParameters.PageSize > 1);
        }

        public static bool HasNext(this GetDestinationsPreviewQuery queryParameters, int totalCount)
        {
            return (queryParameters.PageSize < (int)GetTotalPages(queryParameters, totalCount));
        }

        public static double GetTotalPages(this GetDestinationsPreviewQuery queryParameters, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)queryParameters.PageSize);
        }

        public static bool HasQuery(this GetDestinationsPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.Query);
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