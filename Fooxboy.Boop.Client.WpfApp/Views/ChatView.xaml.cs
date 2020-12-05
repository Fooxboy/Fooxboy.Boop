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
using Fooxboy.Boop.Client.WpfApp.Helpers;
using Fooxboy.Boop.Client.WpfApp.Models;
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ChatView.xaml
    /// </summary>
    public partial class ChatView : Page
    {
        private ChatViewModel _vm;
        
        public ChatView(ChatInfo info, long chatId)
        {
            InitializeComponent();
            _vm = new ChatViewModel(chatId, info);
            this.DataContext = _vm;
        }


        private async void ChatView_OnLoaded(object sender, RoutedEventArgs e)
        {
            Scroll.ToBottom(MessagesListBox);
            await _vm.GetDialogs();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextFieldBox.Text = "";
            Scroll.ToBottom(MessagesListBox);
            await _vm.SendMessage();
        }
    }
}
