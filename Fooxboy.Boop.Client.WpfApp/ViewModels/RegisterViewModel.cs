using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Fooxboy.Boop.Client.WpfApp.Services;

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

        public void Register()
        {
            var api = ApiService.GetApi();

            api.Register.Reg(Nickname, Number, Password, FirstName, LastName);

            api.Register.RegEvent += Register_RegEvent;
        }

        private void Register_RegEvent(Shared.Models.RegisterResponse data)
        {
            MessageBox.Show("Ты пидорас", "Ты зарегистрировава");
        }
    }
}
