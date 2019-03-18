namespace Alpha.Travel.WebApi
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AuthSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public int TokenExpiration { get; set; }

        public int RefreshTokenExpiration { get; set; }
    }
}