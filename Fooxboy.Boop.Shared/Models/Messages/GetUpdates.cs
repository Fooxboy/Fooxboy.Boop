using System.Collections.Generic;

namespace Fooxboy.Boop.Shared.Models.Messages
{
    public class GetUpdates
    {
        public List<Message> Messages { get; set; }
    }
}