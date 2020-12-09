using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
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
        public Dispatcher dispatcher;

        public ChatViewModel(long chatId, ChatInfo info)
        {
            dispatcher = Application.Current.MainWindow.Dispatcher;
            _chatId = chatId;
            Messages = new ObservableCollection<Message>();
            NoMessages1 = Visibility.Visible;
            Info = info;
        }
        public async Task GetDialogs(long count=9999, long offset=0)
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

                StartCheckerMessages();
            }
            catch (BoopRootException e)
            {
                if (e.Code == 9) return;
                MessageBox.Show($"Код ошибки: {e.Code}. Сообщение: {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка");
            }
        }
        
        public void StartCheckerMessages()
        {
            var api = ApiService.Get();
            var serivce = api.Messages.GetLongPollService(ApiService.Get().Address, ApiService.Get().Token);

            serivce.NewMessageEvent += Serivce_NewMessageEvent;
        }

        private void Serivce_NewMessageEvent(List<Message> msgs)
        {
            foreach (var message in msgs)
            {
                if (message.ChatId == 0)
                {
                    //личный диалог
                    if (message.SenderId == _chatId)
                    {
                        dispatcher.BeginInvoke(new Action(() =>
                        {
                            Messages.Add(message);
                            Changed("Messages");
                        }));
                    }
                }
                //todo: групповой чат
               
            }
        }

        public async Task SendMessage()
        {
            
            var api = Services.ApiService.Get();

            if (!string.IsNullOrEmpty(TextMessage))
            {
                try
                {
                    var result = await api.Messages.SendAsync(TextMessage, _chatId);
                    var msg = new Message()
                    {
                        ChatId = 0,
                        ImageSender = AppGlobalConfig.CurrentConnectUser.PathProfilePic,
                        MsgId = result.MsgId,
                        NameSender =
                            $"{AppGlobalConfig.CurrentConnectUser.FirstName} {AppGlobalConfig.CurrentConnectUser.LastName}",
                        RecieverId = _chatId,
                        SenderId = AppGlobalConfig.CurrentConnectUser.UserId,
                        Text = TextMessage,
                        Time = (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                        UsersReaded = ""

                    };
                    Messages.Add(msg);

                    DialogsView.StaticViewModel.UpdateMessage(msg);

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
