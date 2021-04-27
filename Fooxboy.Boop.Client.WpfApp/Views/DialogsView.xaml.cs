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
    /// Логика взаимодействия для DialogsView.xaml
    /// </summary>
    public partial class DialogsView : Page
    {

        public static DialogsViewModel StaticViewModel;
        private DialogsViewModel _vm;
        //private bool isOpen;
        public DialogsView()
        {
            InitializeComponent();
            _vm = new DialogsViewModel();
            DataContext = _vm;
            StaticViewModel = _vm;
        }

        private async void DialogsView_OnLoaded(object sender, RoutedEventArgs e)
        {
           await _vm.GetDialogs();
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            _vm.OpenDialog();
        }
    }
}
