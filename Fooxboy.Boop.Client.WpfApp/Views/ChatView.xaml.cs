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
using Fooxboy.Boop.Client.WpfApp.Helpers;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ChatView.xaml
    /// </summary>
    public partial class ChatView : Page
    {
        private ChatViewModel _vm;
        private long chatId = 0;
        long key = 0;
        long attachId = 0;
        
        public ChatView(ChatInfo info, long chatId)
        {
            InitializeComponent();
            _vm = new ChatViewModel(chatId, info);
            this.DataContext = _vm;
            this.chatId = chatId;
        }


        private async void ChatView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if(chatId > 0)
            {
                Chat.Visibility = Visibility.Visible;
                GroupChat.Visibility = Visibility.Collapsed;
            }
            Scroll.ToBottom(MessagesListBox);
            await _vm.GetDialogs();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextFieldBox.Text = "";
            Scroll.ToBottom(MessagesListBox);
            await _vm.SendMessage(attachId);
            this.key = 0;
            this.attachId = 0;
            IndicatorPhoto.Fill = new SolidColorBrush(Colors.Transparent);
            IndicatorFile.Fill = new SolidColorBrush(Colors.Transparent);

        }

        private async void photoButton_Click(object sender, RoutedEventArgs e)
        {
            var api = ApiService.Get();

            var key = await api.Messages.GetUploadServerAsync(chatId, 1);
            this.key = key.Key;
            this.attachId = key.Id;

            var webClient = new WebClient();
            var pathUrlUpload = "http://" + api.Address + $"/api/msg.uploadAttach/{key.Key}";

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Изрбражения (*.png, *.jpg) | *.png;*.jpg";
            dialog.FilterIndex = 2;

            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document
                IndicatorPhoto.Fill = new SolidColorBrush(Colors.Blue);
                string patchFile = dialog.FileName;
                var a = await webClient.UploadFileTaskAsync(pathUrlUpload, patchFile);
                IndicatorPhoto.Fill = new SolidColorBrush(Colors.GreenYellow);

            }
        }

        private async void fileButton_Click(object sender, RoutedEventArgs e)
        {
            var api = ApiService.Get();

            var key = await api.Messages.GetUploadServerAsync(chatId, 2);
            this.key = key.Key;
            this.attachId = key.Id;
            var webClient = new WebClient();
            var pathUrlUpload = "http://" + api.Address + $"/api/msg.uploadAttach/{key.Key}";

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document
                IndicatorFile.Fill = new SolidColorBrush(Colors.Blue);
                string patchFile = dialog.FileName;
                var a = await webClient.UploadFileTaskAsync(pathUrlUpload, patchFile);
                IndicatorFile.Fill = new SolidColorBrush(Colors.GreenYellow);

            }
        }

        private void InfoGroup_Click(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().GoToChat(new GroupChatInfoView(chatId * -1));
        }
    }
}
