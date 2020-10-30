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
    /// Логика взаимодействия для DialogsMainPage.xaml
    /// </summary>
    public partial class DialogsMainPage : Page
    {
        public DialogsMainPage()
        {
            InitializeComponent();

        }

        private void DialogsMainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().InitChatsFrame(DialogsFrame, ChatFrame);
            Services.NavigationService.GetService().GoToChat("Views/NoSelectChat.xaml");

            Services.NavigationService.GetService().GoToDialogsFrame("Views/DialogsView.xaml");
        }
    }
}
