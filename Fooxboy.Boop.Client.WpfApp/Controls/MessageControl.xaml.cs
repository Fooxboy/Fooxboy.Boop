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
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.Client.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(Message), typeof(MessageControl));

        public Message Message
        {
            get => (Message) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        private void MessageControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextMessage.Text = Message.Text;
            Time.Text = TimeSpan.FromSeconds(Message.Time).ToString(@"hh\:mm");
            NameUser.Text = Message.NameSender;
        }
    }
}
