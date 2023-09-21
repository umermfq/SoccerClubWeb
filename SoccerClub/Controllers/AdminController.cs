using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerClub.Models;

namespace SoccerClub.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
       
        public IActionResult dashboard()
        {
            if(HttpContext.Session.GetInt32("UserID")==null)
            {
                return RedirectToAction("Index", "UserLogin");
            }
            Home home = new Home();
            var logins = _dbContext.GetAllUsers();
            home.Registers = logins;
            return View(home);
        }
        public IActionResult User()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }
            Home home = new Home();
            var logins = _dbContext.GetAllUsers();
            home.Registers = logins;
            return View(home);
        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieScheme");

            //  _session.Abandon();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "/UserLogin");
        }
    }
}
