using TelegramBot.Keyboard;

namespace TelegramBot.Model
{
    public class Response
    {
        public KeyboardMarkup KeyboardMarkup;

        public string Text;

        public static implicit operator Response(string text) => new Response {Text = text};

        public string ImageUrl;
    }
}