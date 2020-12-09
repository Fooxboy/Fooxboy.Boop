using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        public string Nickname { get; set; }
        public ObservableCollection<User> Friends { get; set; }

        public FriendsViewModel()
        {
            Friends = new ObservableCollection<User>();
        }

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
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка");

            }
        }


        public async Task GetFriends()
        {
            var friends = await ApiService.Get().Friends.GetAsync();
            foreach (var friend in friends.Friends)
            {
                Friends.Add(friend);
            }
        }
    }
}
