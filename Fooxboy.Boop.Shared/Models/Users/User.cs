﻿
namespace Fooxboy.Boop.Shared.Models.Users
{
    public class User
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PathProfilePic { get; set; }
        public string LastSeenText { get; set; }
        public long LastSeen { get; set; }
        public string Status { get; set; }
        public long AccessLevel { get; set; }
        public string Specialty { get; set; } //специальность, профессия.
        public string Group { get; set; } //группа
    }
}