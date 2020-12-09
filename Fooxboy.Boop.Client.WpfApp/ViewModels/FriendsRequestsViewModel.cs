using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class FriendsRequestsViewModel:BaseViewModel
    {

        public ObservableCollection<User> RequestsFriends { get; set; }

        public FriendsRequestsViewModel()
        {
            RequestsFriends = new ObservableCollection<User>();
        }

        public async Task LoadRequests()
        {
            var api = ApiService.Get();

            var requests =await  api.Friends.GetRequestsAsync();
            foreach (var request in requests.Friends)
            {
                RequestsFriends.Add(request);
            }

        }
    }
}
