using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Entities;
using MvcDemo.Models;
using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace MvcDemo.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly MvcContext _mvcContext;
        private readonly IConfiguration _configuration;

        public AccountsController(MvcContext mvcContext, IConfiguration configuration)
        {
            _mvcContext = mvcContext;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = MD5SaltAndHash(model.Password);

                User user = _mvcContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);



                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User is locked.");
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }
            }
            return View();
        }

        private string MD5SaltAndHash(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:Md5Salt");

            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register() 
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                if (_mvcContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username),"Username is already exists.");
                }
                string hashedPassword = MD5SaltAndHash(model.Password);

                User user = new()
                {
                    Username = model.Username,
                    Password= hashedPassword 

                };

                _mvcContext.Users.Add(user);

               var affectedRowCount= _mvcContext.SaveChanges();

                if (affectedRowCount==0)
                {
                    ModelState.AddModelError("","User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();

            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            User user = _mvcContext.Users.SingleOrDefault(x => x.Id == userId);

            ViewData["FullName"] = user.FullName;
            ViewData["ProfileImage"] = user.ProfilePictureFileName;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullName([Required][StringLength(50)]string? fullname)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                User user = _mvcContext.Users.SingleOrDefault(x=>x.Id==userId);

                user.FullName = fullname;

                _mvcContext.SaveChanges();

                ViewData["result"] = "Password Changed!";
            }

            ProfileInfoLoader();
            return View("Profile");
        }


        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)] string? password)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                User user = _mvcContext.Users.SingleOrDefault(x => x.Id == userId);

                string hashedPassword = MD5SaltAndHash(password);

                user.Password = hashedPassword;

                _mvcContext.SaveChanges();

                return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public IActionResult ProfileChangeImage([Required]IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                User user = _mvcContext.Users.SingleOrDefault(x => x.Id == userId);

                string fileName = $"p_{userId}.png";
                Stream stream = new FileStream($"wwwroot/Uploads/{fileName}",FileMode.OpenOrCreate);

                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                user.ProfilePictureFileName = fileName;

                _mvcContext.SaveChanges();

                return RedirectToAction(nameof(Profile));

                
            }

            ProfileInfoLoader();
            return View("Profile");
        }


    }
}
