using Newtonsoft.Json.Linq;

namespace TelegramBot.Keyboard
{
    public class InlineKeyboardMarkup : KeyboardMarkup
    {
        public override string ToString()
        {
            var jArray = new JArray();

            for (int i = 0; i < Keyboard.Count; i++)
            {
                if (Keyboard[i].Count < 1) continue;

                jArray.Add(new JArray());

                for (int j = 0; j < Keyboard[i].Count; j++)
                {
                    (jArray[i] as JArray).Add(new JObject()
                    {
                        {"text", Keyboard[i][j].Text},
                        {"callback_data", Keyboard[i][j].CallbackData}
                    });
                }
            }

            return new JObject
            {
                { "inline_keyboard", jArray }
            }.ToString();
        }
    }
}