using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class DialogsViewModel:BaseViewModel
    {
        public ObservableCollection<GetResponse> Dialogs { get; set; }
        public Visibility NoChats { get; set; }

        public async Task GetDialogs()
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
    }
}
