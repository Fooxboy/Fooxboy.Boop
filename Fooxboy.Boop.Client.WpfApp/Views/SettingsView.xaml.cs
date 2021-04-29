using System;
using System.Collections.Generic;
using System.Net;
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

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var api = ApiService.Get();

            var key = await api.Users.GetUploadAvatarUrl();

            var webClient = new WebClient();
            var pathUrlUpload = "http://"+ api.Address + $"/api/users.uploadPhoto/{key}";

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Изрбражения (*.png, *.jpg) | *.png;*.jpg";
            dialog.FilterIndex = 2;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document
                string patchFile = dialog.FileName;
                var a = await webClient.UploadFileTaskAsync(pathUrlUpload, patchFile);
                MessageBox.Show("ready", "ready");
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().BackDialogs();
        }
    }
}
