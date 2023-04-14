using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NgoProjectNew1.Models;
using NgoProjectNew1.Models.ViewModel;


namespace NgoProjectNew1.Controllers
{
    
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly NgoDbContext _context;

        public HomeController(NgoDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginSingupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.NgoRegMembers.Where(e => e.Username == model.Username).SingleOrDefault();
                if (data != null)
                {
                    bool isValid = (data.Username == model.Username && data.Password == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.Username);
                        if (model.Username == "Admin")
                        {
                            HttpContext.Session.SetString("Username", model.Username);
                            return RedirectToAction("Index","Admin");
                        }
                        else
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid Password!";
                        return View(model);
                        
                    }

                }
                else
                {
                    TempData["errorUsername"] = "Invalid Username";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get","Post")]
        public JsonResult UserNameIsExist(string userName)
        {
            var data = _context.NgoRegMembers.Where(e => e.Username == userName).SingleOrDefault();

            if (data != null)
            {
                return Json($"Username {userName} already exist");
            }
            else
            {
                return Json(true);
            }
        }
     
        public IActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new NgoRegMember()
                { 
                    Username = model.Username,
                    ContactNo = model.ContactNo,
                    Address = model.Address,
                    Name = model.Name,
                    Password=model.Password
                };
                _context.NgoRegMembers.Add(data);
                _context.SaveChanges();
                TempData["successMessage"] = "Registartion done successfully";
                return RedirectToAction("Login");
            }
            else 
            {
                TempData["errorMessage"] = "Empty form can't be submitted!";
                return View(model);
            }
        }

     }
 }

