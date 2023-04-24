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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                try { 
            
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
                                return RedirectToAction("Index", "Admin");
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
            catch(Exception e)
            {
                throw new Exception(e.Message);   
                
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get","Post")]
        public IActionResult UserNameIsExist(string userName)
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
            try
            {
                if (ModelState.IsValid)
                {
                    var data = new NgoRegMember()
                    {
                        Username = model.Username,
                        ContactNo = model.ContactNo,
                        Address = model.Address,
                        Name = model.Name,
                        Password = model.Password,
                        CreatedDate = DateTime.Today.Date,
                    };
                    /* var data2 = new Cause()
                     {
                         CauseId = model.MemberId
                     };*/

                    _context.NgoRegMembers.Add(data);
                    //_context.Causes.Add(data2);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Registartion done successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Empty form can't be submitted!";
                    return View(model);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
                
            }
        }

        public IActionResult RaiseACause()
        {

            return View();
        }
        [HttpPost]
        public IActionResult RaiseACause(CausesViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Empty form can't be submitted!";
                    //return View(model);
                    return View(model);
                }

                var userName = HttpContext.Session.GetString("Username");
                var memId = _context.NgoRegMembers.Where(user => user.Username == userName).FirstOrDefault().MemberId;
                var isExist = _context.Causes.Where(user => user.MemberId == memId).FirstOrDefault();

                if (isExist == null)
                {
                    Cause c1 = new Cause();
                    c1.MemberId = memId;
                    c1.RaiserName = model.RaiserName;
                    c1.CauseName = model.CauseName;
                    c1.StartDate = model.StartDate;
                    c1.EndDate = model.EndDate;
                    c1.Category = model.Category;
                    c1.Contact = model.Contact;
                    c1.CauseDesc = model.CauseDesc;

                    _context.Causes.Add(c1);
                }

                else
                {
                    isExist.RaiserName = model.RaiserName;
                    isExist.CauseName = model.CauseName;
                    isExist.StartDate = model.StartDate;
                    isExist.EndDate = model.EndDate;
                    isExist.Category = model.Category;
                    isExist.Contact = model.Contact;
                    isExist.CauseDesc = model.CauseDesc;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }

        

    }
 }

