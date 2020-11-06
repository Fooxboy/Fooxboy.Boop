using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class FriendsViewModel:BaseViewModel
    {
        public string Nickname { get; set; }

        public async Task GetUser()
        {
            var api = Services.ApiService.Get();

        }
    }
}
