using System;
using System.Collections.Generic;
using System.Linq;
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
                ApiService.ChangeToken(result.Token);
                var configService = AppGlobalConfig.ConfigSerivce;
                var config = configService.GetConfig();

                var server = config.Servers.SingleOrDefault(s => s.Address == ApiService.Get().Address);
                if (server != null)
                {
                    config.Servers.Remove(server);
                    server.Token = result.Token;
                    server.NameUser = FirstName + " " + LastName;
                    config.Servers.Add(server);
                    configService.EditConfig(config);
                }
                

               
                Services.NavigationService.GetService().GoTo("Views/DialogsMainPage.xaml");

            }
            catch (BoopRootException e)
            {
                MessageBox.Show($"{e.Code}. {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка");

            }
        }

    }
}
