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
using Fooxboy.Boop.Client.WpfApp.ViewModels;

namespace Fooxboy.Boop.Client.WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для FriendsView.xaml
    /// </summary>
    public partial class FriendsView : Page
    {
        private FriendsViewModel _vm;
        public FriendsView()
        {
            InitializeComponent();
            _vm = new FriendsViewModel();
            DataContext = _vm;

        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await _vm.GetUser();
        }
    }
}
