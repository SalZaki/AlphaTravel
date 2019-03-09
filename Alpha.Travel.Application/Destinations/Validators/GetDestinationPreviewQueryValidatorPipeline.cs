namespace Alpha.Travel.Application.Destinations.Validators
{
    using Common.Validators;
    using Destinations.Models;
    using Destinations.Queries;

    public class GetDestinationPreviewQueryValidatorPipeline : ValidationBehavior<GetDestinationPreviewQuery, DestinationPreviewDto>
    {
        public GetDestinationPreviewQueryValidatorPipeline() : base(new GetDestinationPreviewQueryValidator()) { }
    }
}