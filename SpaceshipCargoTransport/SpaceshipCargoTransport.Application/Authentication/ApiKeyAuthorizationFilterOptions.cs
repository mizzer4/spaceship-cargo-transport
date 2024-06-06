namespace SpaceshipCargoTransport.Application.Authentication
{
    internal class ApiKeyAuthorizationFilterOptions
    {
        public const string SectionName = "ApiKeyAuthorizationFilter";

        public string HeaderName { get; set; } = string.Empty;
    }
}
