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
                ApiService.ChangeAddress(Server.Address);
                ApiService.ChangeToken(Server.Token);
                Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка подключения к серверу.");
            }
            
        }
    }
}
