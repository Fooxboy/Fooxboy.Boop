using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Fooxboy.Boop.BackendServer.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UsersChat> UsersChats { get; set; }
        public DbSet<UnreadMessages> UnreadMessages { get; set; }
        public DbSet<UploadAttachment> UploadAttachments { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=serverdata.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
            
    }
}
