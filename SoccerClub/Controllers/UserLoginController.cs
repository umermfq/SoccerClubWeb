using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoccerClub.Models;
using System.Data;
using System.Web;

namespace SoccerClub.Controllers
{
    public class UserLoginController : Controller
    {
        public string status;
        private readonly ISession _session;
        private readonly IConfiguration _configuration;

        public UserLoginController( IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }
  

        public ISession Get_session()
        {
            return _session;
        }

        [HttpPost]
        public ActionResult Index(Register e)
        {
            int isAdmin;
            string SqlCon = _configuration.GetConnectionString("myCon");

           
            SqlConnection con = new SqlConnection(SqlCon);
            string SqlQuery = "select Email,Password,isAdmin from Register where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read()) // Check if there are any results
            {

                isAdmin = Convert.ToInt32(sdr["isAdmin"]);
                if (isAdmin == 1)
                {
                    // Store the user's email in a session variable
                    _session.SetString("Email", e.Email.ToString());
                    _session.SetString("Password", e.Password.ToString());
                    HttpContext.Session.SetInt32("UserID", e.ID);
                    // Close the connection


                    con.Close();

                    return RedirectToAction("dashboard", "Admin");

                    //return RedirectToAction("RedirectToDashboard");

                }


                else if (isAdmin == 0)
                {
                    _session.SetString("Email", e.Email.ToString());
                    con.Close(); // Close the connection

                    // Redirect to the "Welcome" action
                    return RedirectToAction("", "/");
                }

                
            }
			else
			{

                ViewBag.Message = string.Format("Login Invalid! Try again");
                con.Close();

            }

            return View();
            //return new JsonResult { Data = new { status = status } };  
        }
        public ActionResult RedirectToDashboard()
        {
            // Retrieve the intended redirection URL from the session
            string returnUrl = HttpContext.Session.GetString("ReturnUrl");
            HttpContext.Session.Remove("ReturnUrl");

            HttpContext.Session.Remove("ReturnUrl"); // Clear the session variable

            // Perform any additional logic if needed

            // Redirect to the dashboard or a default page if returnUrl is not valid
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("DefaultAction", "DefaultController"); // Redirect to a default page
            }
        }

        [HttpGet]
        public ActionResult dashboardUser()
        {
            return View();
        }
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieScheme");
            
            //  _session.Abandon();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "UserLogin");
        }
    }
}
