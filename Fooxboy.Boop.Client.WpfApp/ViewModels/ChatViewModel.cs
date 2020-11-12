using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class ChatViewModel:BaseViewModel
    {
        private long _chatId;
        public ObservableCollection<Message> Messages { get; set; }
        public Visibility NoMessages1 { get; set; } 
        public ChatInfo Info { get; set; }
        public string TextMessage { get; set; }

        public ChatViewModel(long chatId, ChatInfo info)
        {
            _chatId = chatId;
            Messages = new ObservableCollection<Message>();
            NoMessages1 = Visibility.Visible;
            Info = info;
        }
        public async Task GetDialogs(long count=20, long offset=0)
        {
            var api = Services.ApiService.Get();

            try
            {
                var dialogs = await api.Messages.GetChatAsync(_chatId, count, offset);

                if (dialogs.Messages.Count > 0)
                {
                    NoMessages1 = Visibility.Hidden;
                    Changed("NoMessages1");
                }

                foreach (var msg in dialogs.Messages)
                {
                    Messages.Add(msg);
                }

                Changed("Messages");
            }
            catch (BoopRootException e)
            {
                if(e.Code == 9) return;
                MessageBox.Show($"Код ошибки: {e.Code}. Сообщение: {e.Message}");
            }
            
        }

        public async Task SendMessage()
        {
            var api = Services.ApiService.Get();

            if (TextMessage != null || TextMessage != string.Empty)
            {
                try
                {
                    var result = await api.Messages.SendAsync(TextMessage, _chatId);
                    Messages.Add(new Message()
                    {
                        ChatId = _chatId,
                        ImageSender =  "null",
                        MsgId =  result.MsgId,
                        NameSender = "Вы",
                        RecieverId = _chatId,
                        SenderId = 1,
                        Text = TextMessage,
                        Time = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                        UsersReaded = ""

                    });

                    Changed("Messages");

                }
                catch (BoopRootException e)
                {
                    MessageBox.Show($"Ошибка при отправке сообщения. Код: {e.Code}. Сообщение: {e.Message}");
                }
            }
        }
    }
}
