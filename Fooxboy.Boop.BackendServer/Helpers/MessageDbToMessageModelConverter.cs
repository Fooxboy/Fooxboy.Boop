using System.Data.Common;
using Fooxboy.Boop.Shared.Models.Messages;

namespace Fooxboy.Boop.BackendServer.Helpers
{
    public static class MessageDbToMessageModelConverter
    {
        public static Message ConvertToMessage(this Database.Message msg)
        {
            var msgModel = new Message();
            msgModel.Text = msg.Text;
            msgModel.Time = msg.Time;
            msgModel.ChatId = msg.ChatId;
            msgModel.ImageSender = msg.ImageSender;
            msgModel.MsgId = msg.MsgId;
            msgModel.NameSender = msg.NameSender;
            msgModel.RecieverId = msg.RecieverId;
            msgModel.SenderId = msg.SenderId;
            msgModel.UsersReaded = msg.UsersReaded;
            msgModel.Attach = msg.Attach;

            return msgModel;
        }
    }
}