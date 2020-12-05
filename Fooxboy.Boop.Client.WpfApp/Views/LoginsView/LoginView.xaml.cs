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
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = LoginViewModel.GetViewModel();
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            LoginBox.Foreground = new SolidColorBrush(Colors.Black);
            LoginBox.Text = "";
        }

        private void PasswordBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            PasswordBox.Password = "";
        }

        private void LoginBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "")
            {
                LoginBox.Foreground = new SolidColorBrush(Colors.Gray);
                LoginBox.Text = "Ваш никнейм";
            }
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "")
            {
                PasswordBox.Foreground = new SolidColorBrush(Colors.Gray);
                PasswordBox.Password = "Ваш пароль";
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginViewModel.GetViewModel().EditPassword(((PasswordBox)sender).Password);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            if (PasswordBox.Password != "Ваш пароль" || LoginBox.Text != "Ваш никнейм")
            {
                grid.Visibility = Visibility.Visible;
                panel.Visibility = Visibility.Collapsed;
                LoginViewModel.GetViewModel().StartAuth();
            }

        }

        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
           Services.NavigationService.GetService().GoTo(new SelectView());
        }
    }
}
