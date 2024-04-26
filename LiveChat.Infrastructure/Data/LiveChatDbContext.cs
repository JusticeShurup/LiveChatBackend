using LiveChat.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Infrastructure.Data
{
    public class LiveChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public LiveChatDbContext(DbContextOptions<LiveChatDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.Messages)
                .WithOne(message => message.User);
            modelBuilder.Entity<Message>()
                .HasOne(message => message.User)
                .WithMany(user => user.Messages);

            base.OnModelCreating(modelBuilder);
        }
    }
}
