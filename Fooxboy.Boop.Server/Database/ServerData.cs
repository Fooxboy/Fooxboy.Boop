using Microsoft.EntityFrameworkCore;

namespace Fooxboy.Boop.Server.Database
{
    public class ServerData:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=serverdata.db");
    }
}