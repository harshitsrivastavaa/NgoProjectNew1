using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgoProjectNew1.Models;
using NgoProjectNew1.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace NgoProjectNew1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly NgoDbContext _context;

        public ValuesController(NgoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/CauseDetails")]
        public IEnumerable<Cause> RaiseDetails()
        {
            IEnumerable<Cause> causeList = _context.Causes.ToList();
            return causeList;
        }
        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(LoginSingupViewModel model)
        {
            try
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
                            return Ok(model);
                        }

                        else
                            return Ok(model);

                    }
                }
                return BadRequest("Error Credentials");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        
        [HttpPost]
        [Route("/RaiseACause")]
        public IActionResult  RaiseACause(CausesViewModel model)
        {
           

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
            return Ok(model);
            
        }



    }
}
