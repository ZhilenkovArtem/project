using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthentificationService
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public UserRepository()
        {
            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Артем",
                LastName = "Жиленков",
                Email = "zhilenkovartem@mail.ru",
                Password = "12345",
                Login = "zhilenkovartem",
                /*Role = new Role()
                {
                    Id = 2,
                    Name = "Администратор"
                }*/
            });

            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivanovivan@mail.ru",
                Password = "54321",
                Login = "ivanovivan",
                /*Role = new Role()
                {
                    Id = 1,
                    Name = "Пользователь"
                }*/
            });

            _users.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Джон",
                LastName = "Джонов",
                Email = "johanovjohn@gmail.com",
                Password = "111",
                Login = "johanovjohn",
                /*Role = new Role()
                {
                    Id = 1,
                    Name = "Пользователь"
                }*/
            });
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(v => v.Login == login);
        }
    }
}
