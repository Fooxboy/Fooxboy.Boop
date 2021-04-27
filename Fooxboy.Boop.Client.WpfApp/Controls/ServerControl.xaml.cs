using System;
using System.Collections.Generic;
using System.Linq;
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
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для ServerControl.xaml
    /// </summary>
    public partial class ServerControl : UserControl
    {
        public ServerControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ServerProperty = DependencyProperty.Register("Server", typeof(ServerInfo), typeof(ServerControl));

        public ServerInfo Server
        {
            get => (ServerInfo) GetValue(ServerProperty);
            set => SetValue(ServerProperty, value);
        }

        private void ServerControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            NameServer.Text = Server.NameServer;
            Address.Text = Server.Address;
            NameUser.Text = Server.NameUser;
            
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Shadow.Opacity = 0.6;
            Shadow.BlurRadius = 35;
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Shadow.Opacity = 0.4;
            Shadow.BlurRadius = 30;
        }

        private async void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LoadingPanel.Visibility = Visibility.Visible;
                infoGrid.Visibility = Visibility.Collapsed;
                ApiService.ChangeAddress(Server.Address);
                ApiService.ChangeToken(Server.Token);
                AppGlobalConfig.CurrentConnectUser = await ApiService.Get().Users.GetInfoAsync(0);
                Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");
            }
            catch (Exception ex)
            {
                LoadingPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Ошибка подключения к серверу.");
            }
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var configService = AppGlobalConfig.ConfigSerivce;
            var config = configService.GetConfig();

            var server = config.Servers.SingleOrDefault(s => s.Address == Server.Address);
            if (server != null)
            {
                config.Servers.Remove(server);
                configService.EditConfig(config);
                MainGrid.Visibility = Visibility.Hidden;
            }
            
        }
    }
}
