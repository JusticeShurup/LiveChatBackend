using LiveChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Repository
{
    public interface IUnitOfWork
    {
        public IRepository<User> Users { get; set; }
        public IRepository<Message> Messages { get; set; }
        void SaveChanges();
    }
}
