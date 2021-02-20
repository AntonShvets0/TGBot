using System.Collections.Generic;
using TelegramBot.Model;
using TelegramBot.Model.Update;

namespace TelegramBot.Interfaces
{
    public interface ICommand
    {
        string Alias { get; set; }
        
        Response Execute(TelegramUser user, List<string> args, UpdateModel update);
    }
}