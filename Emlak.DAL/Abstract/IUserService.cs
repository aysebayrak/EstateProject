using Emlak.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emlak.DAL.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();

        List<User> GetByFilter(string? userName);
        User GetById(string id);

        User Create(User user);
        void Update(string id, User user);
        void Delete(string id);
    }
}
