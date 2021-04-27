using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.Shared.Models.Messages;
using Application = System.Windows.Application;
using Message = Fooxboy.Boop.Shared.Models.Messages.Message;
using MessageBox = System.Windows.MessageBox;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class DialogsViewModel : BaseViewModel
    {
        public ObservableCollection<GetResponse> Dialogs { get; set; }
        public Visibility NoChats { get; set; }
        public Visibility Loading { get; set; }

        public GetResponse SelectItem { get; set; }
        public Dispatcher dispatcher;

        public async Task GetDialogs()
        {
            dispatcher = Application.Current.MainWindow.Dispatcher;
            try
            {
                Loading = Visibility.Visible;
                Changed("Loading");
                NoChats = Visibility.Hidden;
                Changed("NoChats");
                var api = ApiService.Get();
                var dialogs = await api.Messages.GetAsync(10);

                Dialogs = new ObservableCollection<GetResponse>();

                Loading = Visibility.Collapsed;
                Changed("Loading");

                if (dialogs.Chats.Count == 0)
                {
                    NoChats = Visibility.Visible;
                    Changed("NoChats");
                    return;
                }

                foreach (var dialog in dialogs.Chats)
                {
                    Dialogs.Add(dialog);
                }

                Changed("Dialogs");


            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка");
            }


            StartLongPoll();
        }


        public void StartLongPoll()
        {
            var longPollService = ApiService.Get().Messages
                .GetLongPollService(ApiService.Get().Address, ApiService.Get().Token);

            longPollService.StartService();

            longPollService.NewMessageEvent += LongPollServiceOnNewMessageEvent;
        }

        private void LongPollServiceOnNewMessageEvent(List<Message> msgs)
        {

            foreach (var message in msgs)
            {
                if (message.ChatId == 0)
                {
                    //личный диалог
                    var dialog = Dialogs.SingleOrDefault(d => d.ChatId == message.SenderId);

                    if (dialog is null)
                    {
                        //todo: добавляем новый чат в список.
                    }
                    else
                    {
                        dispatcher.BeginInvoke(new Action(() =>
                        {
                            var index = Dialogs.IndexOf(dialog);
                            Dialogs.Remove(dialog);
                            dialog.LastMessageText = message.Text;
                            dialog.Time = message.Time;
                            Dialogs.Add(dialog);
                            Dialogs.Move(index, 0);
                            Changed("Dialogs");

                            // AppGlobalConfig.NotifyIcon.ShowBalloonTip(500, dialog.ChatTitle, dialog.LastMessageText, ToolTipIcon.Info);
                        }));
                    }
                }

                //todo: групповой чат

            }
        }

        public void UpdateMessage(Message message)
        {
            if (message.ChatId == 0)
            {
                //личный диалог
                var dialog = Dialogs.SingleOrDefault(d => d.ChatId == message.RecieverId);

                if (dialog is null)
                {
                    //todo: добавляем новый чат в список.
                }
                else
                {
                    dispatcher.BeginInvoke(new Action(() =>
                    {
                        var index = Dialogs.IndexOf(dialog);
                        Dialogs.Remove(dialog);
                        dialog.LastMessageText = message.Text;
                        dialog.Time = message.Time;
                        Dialogs.Add(dialog);
                        Dialogs.Move(index, 0);
                        Changed("Dialogs");

                        // AppGlobalConfig.NotifyIcon.ShowBalloonTip(500, dialog.ChatTitle, dialog.LastMessageText, ToolTipIcon.Info);
                    }));
                }
            }
        }

        public void OpenDialog()
        {
            try
            {
                if(SelectItem != null)
                {
                    var info = new ChatInfo();
                    info.Image = SelectItem.CoverChat;
                    info.Title = SelectItem.ChatTitle;

                    var navigation = Services.NavigationService.GetService();

                    navigation.GoToChat(new ChatView(info, SelectItem.ChatId));
                }
              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
            
        }
    }
}
