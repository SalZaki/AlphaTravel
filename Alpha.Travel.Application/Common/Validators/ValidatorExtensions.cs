namespace Alpha.Travel.Application.Common.Validators
{
    using FluentValidation;

    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidIntId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x =>
            {
                int id = -1;
                bool success = int.TryParse(x, out id);
                return success && id > 0;
            });
        }

        public static IRuleBuilderOptions<T, int> IsValidIntId<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(x =>
            {
                return x >= 0;
            });
        }
    }
}