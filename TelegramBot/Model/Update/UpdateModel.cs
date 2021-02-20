namespace TelegramBot.Model.Update
{
    public class UpdateModel
    {
        public int Id;

        public MessageModel Message;

        public string CallbackQuery;

        public long CallbackQueryId;
    }
}