namespace TelegramBot.Model
{
    public class TelegramUser
    {
        public int Id;

        public string Username;

        public string FirstName;

        public string LastName;
        
        public object Worker = new object();
    }
}