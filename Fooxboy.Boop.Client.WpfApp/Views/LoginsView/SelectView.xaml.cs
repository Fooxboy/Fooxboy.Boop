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

namespace Fooxboy.Boop.Client.WpfApp.Views.LoginsView
{
    /// <summary>
    /// Логика взаимодействия для SelectView.xaml
    /// </summary>
    public partial class SelectView : Page
    {
        public SelectView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().GoTo("Views/LoginsView/LoginView.xaml");
        }

        private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().GoTo("Views/LoginsView/CheckCodeView.xaml");

        }
    }
}
