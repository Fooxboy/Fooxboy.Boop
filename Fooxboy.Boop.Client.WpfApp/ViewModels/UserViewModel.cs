using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class UserViewModel:BaseViewModel
    {

        public User User { get; set; }
        public string Name { get; set; }

        public async Task LoadUserInfo(long userId)
        {
            var api = ApiService.Get();
            try
            {
                var user = await api.Users.GetInfoAsync(userId);
                User = user;
                Name = $"{user.FirstName} {user.LastName}";
                Changed("Name");
                Changed("User");

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

        public async Task GoToChatUser()
        {
            var nav = Services.NavigationService.GetService();
            var chatInfo = new ChatInfo();
            chatInfo.Image = User.PathProfilePic;
            chatInfo.Title = User.FirstName + " " + User.LastName;

            nav.GoToChat(new ChatView(chatInfo, User.UserId));
        }

        public async Task AddToFriend()
        {
            var api = ApiService.Get();
            await api.Friends.AddAsync(User.UserId);
        }

        public async Task RemoveFromFriendsList()
        {
            var api = ApiService.Get();
            await api.Friends.RemoveAsync(User.UserId);
        }
    }
}
