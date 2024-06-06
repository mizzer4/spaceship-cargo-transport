namespace SpaceshipCargoTransport.Application.Authentication
{
    internal interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
