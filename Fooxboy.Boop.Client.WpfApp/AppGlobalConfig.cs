using System.Windows.Forms;
using Fooxboy.Boop.Client.WpfApp.Services;
using Fooxboy.Boop.Shared.Models.Users;

namespace Fooxboy.Boop.Client.WpfApp
{
    public static class AppGlobalConfig
    {
        public static string Token { get; set; }
        public static string Address { get; set; }
        public static ConfigService ConfigSerivce;
        public static User CurrentConnectUser { get; set; }
        public static NotifyIcon NotifyIcon { get; set; }
    }
}