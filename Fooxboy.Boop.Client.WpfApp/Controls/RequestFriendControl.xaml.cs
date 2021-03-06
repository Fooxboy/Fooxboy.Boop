﻿using System;
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
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.Views;
using Fooxboy.Boop.SDK.Exceptions;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для RequestFriendControl.xaml
    /// </summary>
    public partial class RequestFriendControl : UserControl
    {
        public RequestFriendControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty FriendProperty = DependencyProperty.Register("Friend", typeof(User), typeof(RequestFriendControl));

        public User Friend
        {
            get => (User)GetValue(FriendProperty);
            set => SetValue(FriendProperty, value);
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            ShadowFriend.Opacity = 0.5;
            BackgroundGrid.Visibility = Visibility.Visible;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ShadowFriend.Opacity = 0.0;
            BackgroundGrid.Visibility = Visibility.Collapsed;

        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private async void RequestFriendControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            NameUser.Text = Friend.FirstName + " " + Friend.LastName;
            LastSeen.Text = Friend.LastSeenText;
            UserPhoto.UriSource = new Uri(await ImageHelper.GetImage(Friend.PathProfilePic));
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await ApiService.Get().Friends.ConfirmAsync(Friend.UserId);
                ButtonConf.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка");
            }
           
        }

        private void MessageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var nav = Services.NavigationService.GetService();
            var chatInfo = new ChatInfo();
            chatInfo.Image = Friend.PathProfilePic;
            chatInfo.Title = Friend.FirstName + " " + Friend.LastName;

            nav.GoToChat(new ChatView(chatInfo, Friend.UserId));
        }
    }
}
