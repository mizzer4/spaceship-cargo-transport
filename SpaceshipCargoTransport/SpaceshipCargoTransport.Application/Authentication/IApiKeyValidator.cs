namespace SpaceshipCargoTransport.Application.Authentication
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}
