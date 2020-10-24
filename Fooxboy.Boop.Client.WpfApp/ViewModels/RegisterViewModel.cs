using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.SDK.Exceptions;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class RegisterViewModel:BaseViewModel
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Password { get; set; }

        public static RegisterViewModel _model;

        public static RegisterViewModel GetViewModel()
        {
            return _model ??= new RegisterViewModel();
        }

        public async void Register()
        {
            var api = ApiService.Get();
            try
            {
                var result = await api.Register.StartAsync(Nickname, Password, FirstName, LastName, Number);
                MessageBox.Show($"Ваш Id: {result.UserId}, Token: {result.Token}");

            }
            catch (BoopRootException e)
            {
                MessageBox.Show($"{e.Code}. {e.Message}");
            }
        }

    }
}
