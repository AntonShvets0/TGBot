using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TelegramBot.Model;

namespace TelegramBot.Keyboard
{
    public class ReplyKeyboardMarkup : KeyboardMarkup
    {
        private bool Resize;
        private int User;
        
        public ReplyKeyboardMarkup(TelegramUser user, bool resize = true)
        {
            User = user.Id;
            Resize = resize;
        }
        
        public ReplyKeyboardMarkup(int user, bool resize = true)
        {
            User = user;
            Resize = resize;
        }
        
        public override string ToString()
        {
            var jArray = new JArray();

            lock (Bot.KeyboardLock)
            {
                if (Bot.KeyboardData.ContainsKey(User))
                {
                    Bot.KeyboardData[User] = new Dictionary<string, string>();
                }
                else
                {
                    Bot.KeyboardData.Add(User, new Dictionary<string, string>());
                }
            }


            for (int i = 0; i < Keyboard.Count; i++)
            {
                if (Keyboard[i].Count < 1) continue;

                jArray.Add(new JArray());

                for (int j = 0; j < Keyboard[i].Count; j++)
                {
                    (jArray[i] as JArray).Add(new JArray(Keyboard[i][j].Text));

                    lock (Bot.KeyboardLock)
                    {
                        Bot.KeyboardData[User].Add(Keyboard[i][j].Text, Keyboard[i][j].CallbackData);
                    }
                }
            }

            return new JObject
            {
                { "keyboard", jArray },
                { "resize_keyboar", Resize }
            }.ToString();
        }
    }
}