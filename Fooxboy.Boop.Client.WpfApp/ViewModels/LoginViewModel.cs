using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Fooxboy.Boop.Client.WpfApp.Services;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ErrorString { get; set; }
        public Visibility VisibilityError { get; set; }
        public Visibility VisibilityGrid { get; set; }
        public Visibility VisibilityPanel { get; set; }

        private static LoginViewModel _model;

        private LoginViewModel()
        {
            Login = "Ваш никнейм";
            VisibilityError = Visibility.Collapsed;
            VisibilityGrid = Visibility.Collapsed;
            VisibilityPanel = Visibility.Visible;
        }

        public static LoginViewModel GetViewModel()
        {
            return _model ??= new LoginViewModel();
        }

        public void StartAuth()
        {
            ApiService.GetApi().Login.Logn(Login, Password, false);
            ApiService.GetApi().Login.LognEvent += Login_LognEvent;
        }

        private void Login_LognEvent(Shared.Models.LoginResponse data)
        {
            //todo: ответ пришел.

            
        }

        public void ErrorAuth(string message)
        {
            VisibilityError = Visibility.Visible;
            VisibilityGrid = Visibility.Collapsed;
            VisibilityPanel = Visibility.Visible;
        }

        public void EditPassword(string password)
        {
            Password = password;
        }
    }
}
