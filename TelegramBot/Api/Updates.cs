using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Newtonsoft.Json.Linq;
using TelegramBot.Model;
using TelegramBot.Model.Update;

namespace TelegramBot.Api
{
    public class Updates : Api
    {
        public IEnumerable<UpdateModel> Get(int offset = 0)
        {
            var updates = CallMethod("getUpdates", new NameValueCollection
            {
                {"offset", offset != 0 ? offset.ToString() : null}
            });
            
            foreach (var obj in updates["result"])
            {
                var message = obj["callback_query"] != null ? obj["callback_query"]["message"] : obj["message"];
                var from = obj["callback_query"] != null ? message["chat"] : message["from"];
                
                var model = new UpdateModel
                {
                    Id = (int)obj["update_id"],
                    Message = new MessageModel
                    {
                        Id = (int)message["message_id"],
                        From = new UserModel
                        {
                            Id = (int)from["id"],
                            FirstName = from["first_name"]?.ToString(),
                            LastName = from["last_name"]?.ToString(),
                            Username = from["username"]?.ToString()
                        },
                        Date = (int)message["date"],
                        Text = message["text"]?.ToString()
                    },
                    CallbackQuery = obj["callback_query"]?["data"]?.ToString(),
                    CallbackQueryId = obj["callback_query"] != null ? (long)obj["callback_query"]["id"] : 0,
                };

                lock (Bot.KeyboardLock)
                {
                    if (Bot.KeyboardData.ContainsKey(model.Message.From.Id) 
                        && Bot.KeyboardData[model.Message.From.Id].ContainsKey(model.Message.Text))
                    {
                        model.CallbackQuery = Bot.KeyboardData[model.Message.From.Id][model.Message.Text];
                        Bot.KeyboardData[model.Message.From.Id] = new Dictionary<string, string>();
                    }
                }

                yield return model;
            }
        }
    }
}