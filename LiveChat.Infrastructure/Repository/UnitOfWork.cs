using LiveChat.Application.Repository;
using LiveChat.Domain;
using LiveChat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LiveChatDbContext _dbContext;
        
        public IRepository<User> Users { get; set; }
        public IRepository<Message> Messages { get; set; } 

        public UnitOfWork(LiveChatDbContext dbContext)
        {
            _dbContext = dbContext;
            Users = new Repository<User>(dbContext);
            Messages = new Repository<Message>(dbContext);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
