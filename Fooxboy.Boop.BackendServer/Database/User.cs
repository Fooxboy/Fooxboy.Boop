﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string EMail { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PathProfilePic { get; set; }
        public string Friends { get; set; }
        public string FirendsRequests { get; set; }
        public string FriendsRequested { get; set; }
        public string Status { get; set; }
        public long LastSeen { get; set; }
        public string LastSeenText { get; set; }
        public string KeyUploadAvatar { get; set; }
        public long AccessLevel { get; set; }
        public string Specialty { get; set; } //специальность, профессия.
        public string Group { get; set; } //группа
        public string Position { get; set; } //должность
    }
}
