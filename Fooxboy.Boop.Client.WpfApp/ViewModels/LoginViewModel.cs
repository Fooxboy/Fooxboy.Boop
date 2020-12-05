using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.SDK;
using Fooxboy.Boop.SDK.Exceptions;

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

        public async void StartAuth()
        {
            var api = ApiService.Get();
            try
            {
                var result = await api.Login.StartAsync(Login, Password);
                ApiService.ChangeToken(result.Token);
                Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");
            }
            catch (BoopRootException e)
            {
                Error(e.Code);
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка");

            }
        }

        
        private void Error(int code)
        {
            if(code == 6) ErrorAuth("Не найден пользователь с таким никнеймом");
            else if(code == 7) ErrorAuth("Вы указали неверный пароль");
        }

        public void ErrorAuth(string message)
        {
            VisibilityGrid = Visibility.Collapsed;
            VisibilityPanel = Visibility.Visible;
            MessageBox.Show(message, "Ошибка авторизации");
        }

        public void EditPassword(string password)
        {
            Password = password;
        }
    }
}
