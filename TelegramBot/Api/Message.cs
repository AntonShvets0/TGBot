using System.Collections.Specialized;
using TelegramBot.Keyboard;
using TelegramBot.Model;

namespace TelegramBot.Api
{
    public class Message : Api
    {
        public void Send(int chatId, Response response) => Send(chatId.ToString(), response, false, 0);

        public void Send(
            string chatId, 
            Response response, 
            bool disableNotification, 
            int replyToMessageId
            )
        {
            if (response.ImageUrl != null)
            {
                CallMethod("sendPhoto", new NameValueCollection
                {
                    {"chat_id", chatId},
                    {"photo", response.ImageUrl},
                    {"caption", response.Text},
                    {"disable_notification", disableNotification ? "1" : "0"},
                    {"reply_to_message_id", replyToMessageId.ToString()},
                    {"reply_markup", response.KeyboardMarkup?.ToString()}
                });
                return;
            }
            
            CallMethod("sendMessage", new NameValueCollection
            {
                {"chat_id", chatId},
                {"text", response.Text },
                {"disable_notification", disableNotification ? "1" : "0"},
                {"reply_to_message_id", replyToMessageId.ToString()},
                {"reply_markup", response.KeyboardMarkup?.ToString() }
            });
        }

        public void EditMessage(
            int messageId,
            Response response,
            bool caption = false
        )
        {
            CallMethod(caption ? "editMessageCaption" : "editMesssageText", new NameValueCollection
            {
                {"message_id", messageId.ToString()},
                {caption ? "caption" : "text", response.Text},
                {"reply_markup",response.KeyboardMarkup?.ToString()}
            });
        }

        public void AnswerCallbackQuery(long callbackId, string text, bool showAlert)
        {
            CallMethod("answerCallbackQuery", new NameValueCollection
            {
                {"text", text},
                {"show_alert", showAlert ? "1" : "0"},
                {"callback_query_id", callbackId.ToString()}
            });
        }
    }
}