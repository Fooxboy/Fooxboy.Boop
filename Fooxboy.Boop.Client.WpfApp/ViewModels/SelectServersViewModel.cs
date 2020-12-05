using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Fooxboy.Boop.Shared.Models;

namespace Fooxboy.Boop.Client.WpfApp.ViewModels
{
    public class SelectServersViewModel:BaseViewModel
    {

        public ObservableCollection<ServerInfo> Servers { get; set; }

        public SelectServersViewModel()
        {
            Servers = new ObservableCollection<ServerInfo>();
        }

        public void LoadServers()
        {
            var config = AppGlobalConfig.ConfigSerivce.GetConfig();
            foreach (var server in config.Servers)
            {
                Servers.Add(server);
            }

            Changed("Servers");
        }
    }
}
