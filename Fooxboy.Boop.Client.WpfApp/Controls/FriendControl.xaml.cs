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
using Fooxboy.Boop.Client.WpfApp.Helpers;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для FriendControl.xaml
    /// </summary>
    public partial class FriendControl : UserControl
    {
        public FriendControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty FriendProperty = DependencyProperty.Register("Friend", typeof(User), typeof(FriendControl));

        public User Friend
        {
            get => (User) GetValue(FriendProperty);
            set => SetValue(FriendProperty, value);
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShadowFriend.Opacity = 0.5;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShadowFriend.Opacity = 0.0;

        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var api = Services.ApiService.Get();
            try
            {
                Services.NavigationService.GetService().GoToChat(new UserView(Friend.UserId), Friend.UserId);
            }
            catch (BoopRootException ee)
            {
                MessageBox.Show($"Код ошибки: {ee.Code}. Сообщение: {ee.Message}");
            }
            catch (Exception ee)
            {
                MessageBox.Show($"{ee.Message}", "Ошибка");

            }
        }

        private async void FriendControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            NameUser.Text = Friend.FirstName + " " + Friend.LastName;
            LastSeen.Text = Friend.LastSeenText;
            ImageFriend.UriSource = new Uri(await ImageHelper.GetImage(Friend.PathProfilePic));
        }
    }
}
