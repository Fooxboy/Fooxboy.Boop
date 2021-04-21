using System.ComponentModel.DataAnnotations;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class UploadAttachment
    {
        [Key]
        public long AttachmentId { get; set; }
        public long AttachmentKey { get; set; }
        public long TypeAttach { get; set; }
        public long User { get; set; }
        public string File { get; set; }
        public long ChatId { get; set; }
    }
}