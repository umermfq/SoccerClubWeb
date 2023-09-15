using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerClub.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http;

namespace SoccerClub.Controllers
{
	public class RegisterController : Controller
	{
		public string value = "";

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(Register e)
		{
			
				using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-BUMMN38;Initial Catalog=Football;User ID=sa;Password=Mbe@1234;TrustServerCertificate=True"))
				{
					using (SqlCommand cmd = new SqlCommand("SP_RegisterDetail", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
						cmd.Parameters.AddWithValue("@LastName", e.LastName);
						cmd.Parameters.AddWithValue("@Password", e.Password);
						cmd.Parameters.AddWithValue("@Gender", e.Gender);
						cmd.Parameters.AddWithValue("@Email", e.Email);
						cmd.Parameters.AddWithValue("@Phone", e.PhoneNumber);
						cmd.Parameters.AddWithValue("@SecurityAnwser", e.SecurityAnwser);
						cmd.Parameters.AddWithValue("@isAdmin", e.isAdmin);
						cmd.Parameters.AddWithValue("@status", "INSERT");
						con.Open();
						ViewData["result"] = cmd.ExecuteNonQuery();
						con.Close();
					 }
				}
			
			return View();
		}

	}
}
