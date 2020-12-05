using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Methods;
using Register = Fooxboy.Boop.Shared.Models.Register;

namespace Fooxboy.Boop.SDK
{
    public class Api
    {
        public string Token { get; set; }
        public string Address { get; set; }
        private HttpHelper _httpHelper;
        
        public Methods.Register Register { get; set; }
        public Users Users { get; }
        public Login Login { get; }
        public Messages Messages { get; }
        public Server Server { get; }

        public Api(string address, string token = "")
        {
            _httpHelper = new HttpHelper(address, token);
            this.Register = new Methods.Register(_httpHelper);
            this.Users = new Users(_httpHelper);
            this.Login = new Login(_httpHelper);
            this.Messages =new Messages(_httpHelper);
            this.Server = new Server(_httpHelper);
        }

        public void ChangeAddress(string address)
        {
            Address = address;
            _httpHelper.ChangeAddress("https://" +address);
        }

        public void ChangeToken(string token)
        {
            Token = token;
            _httpHelper.ChangeToken(token);
        }
    }
}