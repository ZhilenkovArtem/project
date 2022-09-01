using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AuthentificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private ILogger _logger;
        private IUserRepository _userRepository;

        public UserController(ILogger logger,
                              IMapper mapper,
                              IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");
        }

        [HttpGet]
        public User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                LastName = "Жиленков",
                FirstName = "Артем",
                Email = "zhilenkovartem@mail.ru",
                Password = "12345",
                Login = "zhilenkovartem"
            };
        }

        [HttpGet]
        [Route("viewmodel")]
        [Authorize]
        public UserViewModel GetUserViewModel()
        {
            User user = GetUser();

            var userViewModel = _mapper.Map<UserViewModel>(user);
            //UserViewModel userViewModel = new UserViewModel(user);

            return userViewModel;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||
              String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = _userRepository.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, 
                "AppCookie", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
