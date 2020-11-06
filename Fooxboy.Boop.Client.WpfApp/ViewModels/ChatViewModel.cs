using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class ChatViewModel:BaseViewModel
    {
        private long _chatId;
        public ObservableCollection<Message> Messages { get; set; }
        public Visibility NoMessages { get; set; } 
        public ChatInfo Info { get; set; }

        public ChatViewModel(long chatId, ChatInfo info)
        {
            _chatId = chatId;
            Messages = new ObservableCollection<Message>();
            NoMessages = Visibility.Visible;
            Info = info;
        }
        public async Task GetDialogs(long count=20, long offset=0)
        {
            var api = Services.ApiService.Get();

            var dialogs = await api.Messages.GetChatAsync(_chatId, count, offset);

            if (dialogs.Messages.Count > 0)
            {
                NoMessages = Visibility.Visible;
                Changed("NoMessages");
            }

            foreach (var msg in dialogs.Messages)
            {
                Messages.Add(msg);
            }

            Changed("Messages");
        }
    }
}
