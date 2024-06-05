namespace SpaceshipCargoTransport.Application.Authentication
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        public bool IsValid(string apiKey)
        {
            return !string.IsNullOrEmpty(apiKey);
        }
    }
}
