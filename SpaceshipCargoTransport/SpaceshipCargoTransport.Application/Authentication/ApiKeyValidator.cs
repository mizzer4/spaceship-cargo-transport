namespace SpaceshipCargoTransport.Application.Authentication
{
    internal class ApiKeyValidator : IApiKeyValidator
    {
        public bool IsValid(string apiKey)
        {
            return !string.IsNullOrEmpty(apiKey);
        }
    }
}
