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
using Fooxboy.Boop.SDK.Exceptions;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для DialogsMainPage.xaml
    /// </summary>
    public partial class DialogsMainPage : Page
    {
        public DialogsMainPage()
        {
            InitializeComponent();

        }

        private void DialogsMainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().InitChatsFrame(DialogsFrame, ChatFrame);
            Services.NavigationService.GetService().GoToChat("Views/NoSelectChat.xaml");

            Services.NavigationService.GetService().GoToDialogsFrame("Views/DialogsView.xaml");
        }

        private void Chats_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextChat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4169E1"));
            TextFriends.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextSettings.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextUser.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            Services.NavigationService.GetService().GoToDialogsFrame("Views/DialogsView.xaml");
        }

        private void Friends_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextChat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextFriends.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4169E1"));
            TextSettings.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextUser.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));

            Services.NavigationService.GetService().GoToDialogsFrame("Views/FriendsView.xaml");
        }

        private void User_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextChat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextFriends.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextSettings.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextUser.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4169E1"));

            try
            {
                Services.NavigationService.GetService().GoToChat(new UserView(AppGlobalConfig.CurrentConnectUser.UserId), AppGlobalConfig.CurrentConnectUser.UserId);
            }
            catch (BoopRootException ex)
            {
                MessageBox.Show($"Код ошибки: {ex.Code}. Сообщение: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка");

            }
        }

        private void Settings_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextChat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextFriends.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            TextSettings.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4169E1"));
            TextUser.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));

            Services.NavigationService.GetService().GoToChat(new SettingsView());

        }
    }
}
