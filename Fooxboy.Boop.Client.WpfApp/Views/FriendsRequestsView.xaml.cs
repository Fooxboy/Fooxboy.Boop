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
    /// Логика взаимодействия для FriendsRequestsView.xaml
    /// </summary>
    public partial class FriendsRequestsView : Page
    {
        private FriendsRequestsViewModel _vm;
        public FriendsRequestsView()
        {
            InitializeComponent();
            _vm = new FriendsRequestsViewModel();
            DataContext = _vm;
        }


        private async void FriendsRequestsView_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.LoadRequests();
        }
    }
}
