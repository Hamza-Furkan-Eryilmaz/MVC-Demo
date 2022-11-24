using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Entities;
using MvcDemo.Helpers;
using MvcDemo.Models;
using System.Linq;

namespace MvcDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly MvcContext _mvcContext;
        private readonly IMapper _mapper;
        private readonly IHasher _hasher;

        public UserController(MvcContext mvcContext, IMapper mapper, IHasher hasher)
        {
            _mvcContext = mvcContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public IActionResult Index()
        {
            
            List<UserViewModel>users=_mvcContext.Users.ToList()
                .Select(x=>_mapper.Map<UserViewModel>(x)).ToList();

            return View(users);
        }

        public IActionResult Create() 
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_mvcContext.Users.Any(x=>x.Username.ToLower()==model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists");
                    return View(model);
                }

                User user=_mapper.Map<User>(model);
                user.Password=_hasher.MD5SaltAndHash(model.Password);
                _mvcContext.Users.Add(user);
                _mvcContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            User user = _mvcContext.Users.Find(id);
            EditUserViewModel model =_mapper.Map<EditUserViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_mvcContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()&&x.Id!=id))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists");
                    return View(model);
                }

                User user = _mvcContext.Users.Find(id);
                _mapper.Map(model,user);
                _mvcContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

      
        public IActionResult Delete(Guid id)
        {
           
            
                User user = _mvcContext.Users.Find(id);
                if (user!=null)
                {

                    _mvcContext.Users.Remove(user);
                    _mvcContext.SaveChanges();

                
            }
            return RedirectToAction(nameof(Index));



        }
    }
}
