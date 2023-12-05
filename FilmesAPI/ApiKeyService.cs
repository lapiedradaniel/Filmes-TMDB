namespace FilmesAPI
{
    public class ApiKeyService
    {
        public string ApiKey { get; }
        public string Token { get; }

        public ApiKeyService(string apiKey, string token)
        {
            ApiKey = apiKey;
            Token = token;
        }
    }
}
