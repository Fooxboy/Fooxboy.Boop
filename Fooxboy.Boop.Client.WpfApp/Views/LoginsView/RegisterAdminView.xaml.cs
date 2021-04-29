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
using System.Linq;
using System.Windows.Shapes;

namespace Fooxboy.Boop.Client.WpfApp.Views.LoginsView
{
    /// <summary>
    /// Логика взаимодействия для RegisterAdminView.xaml
    /// </summary>
    public partial class RegisterAdminView : Page
    {
        private long _code;
        public RegisterAdminView(long code)
        {
            _code = code;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Loading.Visibility = Visibility.Visible;
            ContentGrid.Visibility = Visibility.Collapsed;
            try
            {
                var api = Services.ApiService.Get();

                var result = await api.Register.Admin(nickname.Text, "null", email.Text, firstName.Text, lastName.Text, position.Text, password.Text, _code);

                Services.ApiService.ChangeToken(result.Token);
                var configService = AppGlobalConfig.ConfigSerivce;
                var config = configService.GetConfig();

                var server = config.Servers.SingleOrDefault(s => s.Address == Services.ApiService.Get().Address);
                if (server != null)
                {
                    config.Servers.Remove(server);
                    server.Token = result.Token;
                    server.NameUser = firstName.Text + " " + lastName.Text;
                    config.Servers.Add(server);
                    configService.EditConfig(config);
                }

                AppGlobalConfig.CurrentConnectUser = await api.Users.GetInfoAsync(0);
                Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");
            }catch(Exception ex)
            {
                Loading.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
                MessageBox.Show($"ИСКЛЮЧЕНИЕ: {ex.Message} \n Stack Trace: \n {ex.StackTrace}", $"Произошла ошибка {ex.GetType()}");
            }

            
        }
    }
}
