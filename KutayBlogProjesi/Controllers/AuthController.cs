using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KutayBlogProjesi.Context;
using KutayBlogProjesi.Models.Entities.Concrete;
using KutayBlogProjesi.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KutayBlogProjesi.Controllers
{
    public class AuthController : Controller
    {
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string rPATH)
        {
            ViewBag.rPATH = rPATH;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO model, string rPATH)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));

                if (user is not null)
                {
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("username", user.Username);

                    if (string.IsNullOrEmpty(rPATH)) return RedirectToAction("Index", "Home");
                    else return Redirect(rPATH);
                }
                else ModelState.AddModelError("", "This user does not exist");
            }
            else ModelState.AddModelError("", "This user does not exist");
            return View();
        }

        [HttpPost]
        public IActionResult Edit(UserUpdateDTO model, string rPATH)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(x => x.Email.Equals(model.Email));

                if (model.Password.Equals(user.Password)) 
                {
                    user.Email = model.Email.ToLower();
                    user.Username = model.Username;
                    return View(rPATH);
                }
                else
                    TempData["message"] = "Unable to update";
                    return View();
            }
            else TempData["message"] = "Unable to update"; 
            return View();
        }

        [HttpGet("[controller]/[action]/{username}")]
        public IActionResult AccountSettings()
        {

            return View();
        }


        //public IActionResult Edit()
        //{
        //    User user = _context.Users.FirstOrDefault(x => x.Email.Equals(y); 

        //    UserUpdateDTO userUpdateDTO = new(user);

        //    return View(userUpdateDTO);
        //}


        //[HttpPost]
        //public IActionResult Edit(UserUpdateDTO model, string rPath)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = _context.Users.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
        //    }
        //} beceremedik abi



        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterDTO user)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Users.Any(x => x.Email.ToLower().Equals(user.Email.ToLower())))
                {
                    User newUser = new User(user.Email, user.Password);
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    TempData["message"] = "Registeration Successfull";
                    return RedirectToAction("Login");
                }
                else ModelState.AddModelError("", "This username already exists.");
            }
            return View();

        }

    }
}
