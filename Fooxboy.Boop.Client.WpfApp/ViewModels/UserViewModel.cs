using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fooxboy.Boop.Client.WpfApp.Services;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        public async Task LoadUserInfo(long userId)
        {
            var api = ApiService.Get();
            var user = await api.Users.GetInfoAsync(userId);
        }
    }
}
