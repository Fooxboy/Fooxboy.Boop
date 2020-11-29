using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class DialogsViewModel:BaseViewModel
    {
        public ObservableCollection<GetResponse> Dialogs { get; set; }
        public Visibility NoChats { get; set; }

        public GetResponse SelectItem { get; set; }

        public async Task GetDialogs()
        {
            try
            {
                NoChats = Visibility.Hidden;
                Changed("NoChats");
                var api = ApiService.Get();
                var dialogs = await api.Messages.GetAsync(10);

                Dialogs = new ObservableCollection<GetResponse>();

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
            var longPollService = ApiService.Get().Messages.GetLongPollService();

            longPollService.StartService();

            longPollService.NewMessageEvent += LongPollServiceOnNewMessageEvent;
        }

        private void LongPollServiceOnNewMessageEvent(List<Message> msgs)
        {
            //todo: proccessing newMessages.

            foreach (var message in msgs)
            {
                var dialog = Dialogs.SingleOrDefault(d => d.ChatId == message.ChatId);

                if (dialog is null)
                {
                    //todo: добавляем новый чат в список.
                }
                else
                {
                    Dialogs.Remove(dialog);
                    dialog.LastMessageText = message.Text;
                    dialog.Time = message.Time;
                    Dialogs.Add(dialog);
                    var index = Dialogs.IndexOf(dialog);
                    Dialogs.Move(index, 0);
                    Changed("Dialogs");
                }
            }
        }

        public void OpenDialog()
        {
            var info = new ChatInfo();
            info.Image = "null";
            info.Title = SelectItem.ChatTitle;

            var navigation = Services.NavigationService.GetService();

            navigation.GoToChat(new ChatView(info, SelectItem.ChatId));
        }
    }
}
