﻿using System.Collections.Specialized;
using TelegramBot.Keyboard;

namespace TelegramBot.Api
{
    public class Message : Api
    {
        public void Send(
            string chatId, 
            string text, 
            bool disableNotification, 
            int replyToMessageId,
            KeyboardMarkup keyboardMarkup
            )
        {
            CallMethod("sendMessage", new NameValueCollection
            {
                {"chat_id", chatId},
                {"text", text},
                {"disable_notification", disableNotification ? "1" : "0"},
                {"reply_to_message_id", replyToMessageId.ToString()},
                {"reply_markup", keyboardMarkup?.ToString() }
            });
        }

        public void EditMessage(
            int messageId,
            string text,
            InlineKeyboardMarkup keyboardMarkup
            )
        {
            CallMethod("editMesssageText", new NameValueCollection
            {
                {"message_id", messageId.ToString()},
                {"text", text},
                {"reply_markup", keyboardMarkup.ToString()}
            });
        }
    }
}