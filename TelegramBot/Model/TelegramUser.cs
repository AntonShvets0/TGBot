namespace TelegramBot.Model
{
    public class TelegramUser
    {
        public int Id;

        public string Username;

        public string FirstName;

        public string LastName;
        
        public object Worker = new object();

        public Bot Bot;


        public string ResponseData;
        public bool ResponseToVar;

        public string ReadMessage()
        {
            ResponseToVar = true;
            while (ResponseData == null) { }

            ResponseToVar = false;
            return ResponseData;
        }
    }
}