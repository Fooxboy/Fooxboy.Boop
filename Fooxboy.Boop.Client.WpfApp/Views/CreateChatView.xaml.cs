using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
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

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateChatView.xaml
    /// </summary>
    public partial class CreateChatView : Page
    {

        private string PatchImage;
        public CreateChatView()
        {
            InitializeComponent();
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Изрбражения (*.png, *.jpg) | *.png;*.jpg";
            dialog.FilterIndex = 2;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document
                string patchFile = dialog.FileName;

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.DecodePixelHeight = 150;
                bitmap.DecodePixelWidth = 150;
                bitmap.UriSource = new Uri(patchFile);

                bitmap.EndInit();

                Pic.Source = bitmap;

                this.PatchImage = patchFile;

            }
        }

        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            nextButton.Visibility = Visibility.Collapsed;
            loading.Visibility = Visibility.Visible;

            var api = ApiService.Get();
            var info = await api.Messages.CreateChat(nameChat.Text);
            
            if(PatchImage != null)
            {
                var infoFile = await api.Messages.GetUploadPhotoChatUrl(info.ChatId);

                var webClient = new WebClient();
                var pathUrlUpload = "http://" + api.Address + $"/api/msg.uploadChatPhoto/{infoFile.Key}";
                var a = await webClient.UploadFileTaskAsync(pathUrlUpload, PatchImage);
            }else
            {

            }

            var infoChatOpen = new ChatInfo();
            infoChatOpen.Image = PatchImage;
            infoChatOpen.Title = info.Title;

            var navigation = Services.NavigationService.GetService();

            navigation.GoToChat(new ChatView(infoChatOpen, info.ChatId * -1));
            navigation.GoToDialogsFrame(new DialogsView());

        }
    }
}
