using System.Collections.Generic;
using TelegramBot.Interfaces;
using TelegramBot.Keyboard;
using TelegramBot.Model;
using TelegramBot.Model.Update;

namespace ExampleBot.Commands
{
    public class HelloCommand : ICommand
    {
        public string Alias { get; set; } = "hello";

        public Response Execute(TelegramUser user, List<string> args, UpdateModel update)
        {
            var keyboard = new InlineKeyboardMarkup();
            keyboard.AddButton("Test", "/heas");

            return new Response()
            {
                KeyboardMarkup = keyboard,
                Text = "Hello world"
            };
        }
    }
}