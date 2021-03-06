﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using TelegramBot.Enums;
using TelegramBot.Interfaces;
using TelegramBot.Model;
using TelegramBot.Model.Update;

namespace TelegramBot
{
    public class Bot
    {
        public ApiManager Api;
        public HashSet<TelegramUser> Users = new HashSet<TelegramUser>();
        public HashSet<ICommand> Commands = new HashSet<ICommand>();

        private int OffsetId = 0;

        private Dictionary<Error, string> _errorData = new Dictionary<Error, string>();

        public static Dictionary<int, Dictionary<string, string>> KeyboardData
            = new Dictionary<int, Dictionary<string, string>>();

        public static object KeyboardLock = new object();
        
        public Bot(string accessToken, string nsCommand, Assembly assembly)
        {
            Api = new ApiManager(accessToken);
            
            var command = assembly.GetTypes().Where(p =>
                p.Namespace == nsCommand && p.GetInterfaces().Any(a => a.FullName == "TelegramBot.Interfaces.ICommand"));


            foreach (var c in command)
            {
                Commands.Add(Activator.CreateInstance(c) as ICommand);
            }
        }

        public void RegisterError(Error error, string message)
        {
            if (_errorData.ContainsKey(error)) _errorData[error] = message;
            else _errorData.Add(error, message);
        }

        public string GetError(Error error)
        {
            if (_errorData.ContainsKey(error)) return _errorData[error];
            
            return error.ToString();
        }

        public void Start()
        {
            while (true)
            {
                foreach (var update in Api.Updates.Get(OffsetId))
                {
                    var user = Users.FirstOrDefault(u => u.Id == update.Message.From.Id);
                    OffsetId = update.Id + 1;

                    if (
                        (!user?.ResponseToVar ?? true) 
                        && update.CallbackQuery == null 
                        && (update.Message.Text == null || !update.Message.Text.StartsWith("/")))
                        continue;

                    if (user != null)
                    {
                        HandleUser(user, update);
                    }
                    else
                    {
                        user = CreateUser(update);
                        HandleUser(user, update);
                    }
                }
            }
        }

        private void HandleUser(TelegramUser user, UpdateModel update)
        {
            new Thread(() =>
            {
                if (user.ResponseToVar)
                {
                    user.ResponseData = update.Message.Text;
                    return;
                }
                
                lock (user.Worker)
                {
                    if (update.CallbackQueryId != 0)
                    {
                        Api.Message.AnswerCallbackQuery(update.CallbackQueryId, null, false);
                    }
                    
                    var args = 
                        update.CallbackQuery != null ?
                            update.CallbackQuery.Split(' ').ToList() 
                            :
                            update.Message.Text.Substring(1).Split(' ').ToList();

                    var command = Commands.FirstOrDefault(e => e.Alias == args[0]);
                    if (command == null)
                    {
                        Api.Message.Send(update.Message.From.Id, GetError(Error.UnknownCommand));
                        return;
                    }
                    
                    args.RemoveAt(0);
                    var response = command.Execute(user, args, update);

                    if (response != null) Api.Message.Send(update.Message.From.Id, response);
                }
            }).Start();
        }

        private TelegramUser CreateUser(UpdateModel update)
        {
            var u = new TelegramUser
            {
                Id = update.Message.From.Id,
                Username = update.Message.From.Username,
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName,
                Bot = this
            };

            Users.Add(u);

            return u;
        }
    }
}