using System.Collections.Generic;
using Fooxboy.Boop.Server.Database;
using Fooxboy.Boop.Server.Services;
using ProtoBuf;

namespace Fooxboy.Boop.Server.Models
{
    public interface ICommand
    {
         string Command { get; }
         int Id { get; }
         ResponseCommand Execute(object data, User user, LoggerService logger);
    }
}