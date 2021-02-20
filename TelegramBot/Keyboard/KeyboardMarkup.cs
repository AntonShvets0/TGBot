using System.Collections.Generic;

namespace TelegramBot.Keyboard
{
    public abstract class KeyboardMarkup
    {
        protected List<List<KeyboardButton>> Keyboard = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
        };

        public int MaxInRow = 4;
        
        public void AddButton(string text, string callbackData)
        {
            if (Keyboard[Keyboard.Count - 1].Count >= MaxInRow) NewLine();
            
            Keyboard[Keyboard.Count - 1].Add(new KeyboardButton
            {
                Text = text,
                CallbackData = callbackData
            });
        }

        public void AddButton(KeyboardButton keyboardButton)
        {
            if (Keyboard[Keyboard.Count - 1].Count >= MaxInRow) NewLine();
            
            Keyboard[Keyboard.Count - 1].Add(keyboardButton);
        }

        public void NewLine()
        {
            Keyboard.Add(new List<KeyboardButton>());
        }
    }
}