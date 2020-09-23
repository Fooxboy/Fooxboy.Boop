using Fooxboy.Boop.SDK.Helpers;
using Fooxboy.Boop.SDK.Models;

namespace Fooxboy.Boop.SDK.Methods
{
    public class Messages
    {
        public event EventDelegate SendEvent;
        public event EventDelegate GetEvent;
        public event EventDelegate GetChatEvent;
        public void Send(string text, long chatId)
        {
            var model = new Shared.Models.Messages.Send();
            model.Text = text;
            model.ChatId = chatId;
            
            SenderHelper.GetHelper().Send("msg.snd", model, "msg.send");
        }

        public void Get(long count, long offset, bool onlyUnread = false)
        {
            var model = new Shared.Models.Messages.Get();
            model.Count = count;
            model.Offset = offset;
            model.OnlyUnread = onlyUnread;
            
            SenderHelper.GetHelper().Send("msg.get", model, "msg.get");
        }

        public void GetChat(long chatId, long count, long offset)
        {
            var model = new Shared.Models.Messages.GetChat();
            model.Count = count;
            model.Offset = offset;
            model.ChatId = chatId;
            
            SenderHelper.GetHelper().Send("msg.getChat", model, "msg.getChat");
        }
    }
}