using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class FriendsViewModel:BaseViewModel
    {
        public string Nickname { get; set; }

        public async Task GetUser()
        {
            var api = Services.ApiService.Get();
            try
            {
                var idUser = await api.Users.GetIdAsync(Nickname);
                Services.NavigationService.GetService().GoToChat(new UserView(idUser), idUser);
            }
            catch (BoopRootException e)
            {
                MessageBox.Show($"Код ошибки: {e.Code}. Сообщение: {e.Message}");
            }
        }
    }
}
