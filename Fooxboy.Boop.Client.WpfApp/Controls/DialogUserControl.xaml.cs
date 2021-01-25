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
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для DialogUserControl.xaml
    /// </summary>
    public partial class DialogUserControl : UserControl
    {
        public DialogUserControl()
        {
            InitializeComponent();
            
        }

        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(GetResponse), typeof(DialogUserControl));

        public GetResponse Dialog
        {
            get => (GetResponse)GetValue(DialogProperty);
            set => SetValue(DialogProperty, value);
        }

        private async void DialogUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TitleChat.Text = Dialog.ChatTitle;
            LastMessage.Text = Dialog.LastMessageText;
            var uri = await ImageHelper.GetImage(Dialog.CoverChat);
            var urii = new Uri(uri);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.DecodePixelHeight = 60;
            bitmap.DecodePixelWidth = 60;
            bitmap.UriSource = urii;
            bitmap.EndInit();
            photoUser.ImageSource = bitmap;

            var a = 0;
        }
    }
}
