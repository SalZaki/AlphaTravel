namespace Alpha.Travel.WebApi.Filters.Exceptions
{
    public abstract class BaseExceptionFilter
    {
        public string ErrorTitle { get; }

        public ApiSettings ApiSettings { get; }

        public BaseExceptionFilter(ApiSettings apiSettings, string errorTitle)
        {
            ApiSettings = apiSettings;
            ErrorTitle = errorTitle;
        }
    }
}