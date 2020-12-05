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

namespace Fooxboy.Boop.Client.WpfApp.Views.LoginsView
{
    /// <summary>
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Page
    {
        public RegisterView()
        {
            InitializeComponent();
            DataContext = RegisterViewModel.GetViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            RegisterViewModel.GetViewModel().Register();
        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            Services.NavigationService.GetService().GoTo(new SelectView());
        }
    }
}
