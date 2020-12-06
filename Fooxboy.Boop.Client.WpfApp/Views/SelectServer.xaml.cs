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
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SelectServer.xaml
    /// </summary>
    public partial class SelectServer : Page
    {
        private SelectServersViewModel _vm;
        public SelectServer()
        {
            InitializeComponent();
            _vm = new SelectServersViewModel();
            DataContext = _vm;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GridConnect.Visibility = Visibility.Visible;
            try
            {
                Services.ApiService.ChangeAddress(AddressServer.Text);
                var api = ApiService.Get();
                var info = await api.Server.InfoAsync();
                info.Address = AddressServer.Text;
                var configService = AppGlobalConfig.ConfigSerivce;
                var config = configService.GetConfig();
                if (config.Servers.Any(s => s.Address == AddressServer.Text))
                {
                    var serverinfo = config.Servers.Single(s => s.Address == AddressServer.Text);
                    if (serverinfo.Token != "")
                    {
                        ApiService.ChangeToken(serverinfo.Token);
                        AppGlobalConfig.CurrentConnectUser = await api.Users.GetInfoAsync(0);
                        Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");
                    }
                    else
                    {
                        Services.NavigationService.GetService().GoTo("Views/LoginsView/SelectView.xaml");
                    }
                   
                }
                else
                {

                    config.Servers.Add(info);
                    configService.EditConfig(config);
                    
                    Services.NavigationService.GetService().GoTo("Views/LoginsView/SelectView.xaml");
                }
                
            }
            catch (Exception ex)
            {
                GridConnect.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Ошибка подключения к серверу");
            }
            

        }

        private void SelectServer_OnLoaded(object sender, RoutedEventArgs e)
        {
           _vm.LoadServers();
        }
    }
}
