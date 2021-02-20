using TelegramBot.Api;

namespace TelegramBot
{
    public class ApiManager
    {
        public string AccessToken { get; }

        public Message Message { get; }

        public Updates Updates { get; }

        public ApiManager(string accessToken)
        {
            AccessToken = accessToken;

            Message = new Message
            {
                AccessToken = AccessToken
            };

            Updates = new Updates
            {
                AccessToken = AccessToken
            };
        }
    }
}