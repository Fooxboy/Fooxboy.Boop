using Fooxboy.Boop.Client.WpfApp.Helpers;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Shared.Models.Users;
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

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupChatInfoView.xaml
    /// </summary>
    public partial class GroupChatInfoView : Page
    {
        private long _chatId;
        string photo = string.Empty;
        string titile = string.Empty;
        public GroupChatInfoView(long chatId)
        {
            _chatId = chatId;
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingChat.Visibility = Visibility.Visible;
            InfoChat.Visibility = Visibility.Collapsed;

            var api = Services.ApiService.Get();

            var chat = await api.Messages.GetChatInfo(_chatId);
            titile = chat.Title;
            TitleChat.Text = chat.Title;
            var listUsers = new List<User>();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.DecodePixelHeight = 100;
            bitmap.DecodePixelWidth = 100;
            photo = await ImageHelper.GetImage(chat.Cover);
            bitmap.UriSource = new Uri(photo);
            bitmap.EndInit();


            photoChat.ImageSource = bitmap;

            int countMembers = 0;
            foreach (var item in chat.Members.Split(","))
            {
                if(item != "")
                {
                    var usr = await api.Users.GetInfoAsync(long.Parse(item));
                    MembersList.Items.Add(usr);
                    countMembers++;
                }
            }

            CountMembers.Text = countMembers.ToString() + " участников";

            LoadingChat.Visibility = Visibility.Collapsed;
            InfoChat.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var chatinfo = new ChatInfo();
            chatinfo.Image = photo;
            chatinfo.Title = titile;
            Services.NavigationService.GetService().GoToChat(new ChatView(chatinfo, _chatId * -1));
        }
    }
}
