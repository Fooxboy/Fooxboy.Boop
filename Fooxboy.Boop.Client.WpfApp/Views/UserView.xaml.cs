using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : Page
    {
        private long _userId;
        private UserViewModel _vm;
        private bool IsFriend;
        public UserView(long userId)
        {
            InitializeComponent();
            _userId = userId;
            _vm = new UserViewModel();
            DataContext = _vm;
        }


        private async void UserView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_userId == AppGlobalConfig.CurrentConnectUser.UserId)
            {
                await _vm.LoadUserInfo(_userId);
                FriendButton.Visibility = Visibility.Collapsed;
                MessageButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                IsFriend = await ApiService.Get().Users.IsFriendAsync(_userId);
                await _vm.LoadUserInfo(_userId);

                if (IsFriend) FriendButton.Content = "Удалить из друзей";
                else FriendButton.Content = "Добавить в друзья";
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await _vm.GoToChatUser();
        }

        private async void FriendButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsFriend)
            {
                await _vm.RemoveFromFriendsList();
            }
            else await _vm.AddToFriend();
        }
    }
}
