using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fooxboy.Boop.Client.WpfApp.Services;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class DialogsViewModel:BaseViewModel
    {
        public async void GetDialogs()
        {
            var api = ApiService.Get();
            var dialogs = await api.Messages.GetAsync(10);

            
        }
    }
}
