using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthentificationService
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}
