namespace Alpha.Travel.Application.Common.Queries
{
    using System;
    using System.Linq;

    public static class BaseGetPreviewQueryExtensions
    {
        public static bool HasPrevious(this BaseGetPreviewQuery queryParameters)
        {
            return (queryParameters.PageNumber > 1);
        }

        public static bool HasNext(this BaseGetPreviewQuery queryParameters, int totalCount)
        {
            return (queryParameters.PageNumber < (int)GetTotalPages(queryParameters, totalCount));
        }

        public static double GetTotalPages(this BaseGetPreviewQuery queryParameters, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)queryParameters.PageSize);
        }

        public static bool HasQuery(this BaseGetPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.Query);
        }

        public static bool HasOrder(this BaseGetPreviewQuery queryParameters)
        {
            return !string.IsNullOrEmpty(queryParameters.OrderBy);
        }

        public static bool IsDescending(this BaseGetPreviewQuery queryParameters)
        {
            if (!string.IsNullOrEmpty(queryParameters.OrderBy))
            {
                return queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
            }
            return false;
        }
    }
}