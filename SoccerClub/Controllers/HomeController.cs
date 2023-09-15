using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using SoccerClub.Models;
using System;
using System.Diagnostics;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoccerClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _dbContext;
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
		{
            _logger = logger;
			_dbContext = dbContext;
		}

        public IActionResult Dashboard()
        {
            Home home = new Home();
            var logins = _dbContext.GetAllUsers();
            home.logins = logins;
            return View(home);
        }
        public IActionResult Main()
        {
			Home home = new Home();
			var teams = _dbContext.GetAllTeams();
			home.teams = teams;
        return View(home);
          
        }
		public IActionResult TeamDetails(int teamId)
		{
			var teamDetails = new team(); // Replace "Team" with your actual entity class name
			var teamNames = new List<team>(); // Replace "Team" with your actual entity class name

			var query = @"
        SELECT t.ID, l.Name AS LeagueName, t.TeamName, t.TeamFlagImage,t.TeamDesc
        FROM Team t
        INNER JOIN Season s ON t.SeasonID = s.ID
        INNER JOIN League l ON s.LeagueID = l.ID
        WHERE t.ID = @teamId";

			using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = query;
				command.Parameters.Add(new SqlParameter("@teamId", teamId));
				command.CommandType = System.Data.CommandType.Text;

				_dbContext.Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						teamDetails.Id = Convert.ToInt32(result["ID"]);
						teamDetails.Leaguename = Convert.ToString(result["LeagueName"]);
						teamDetails.teamname = Convert.ToString(result["TeamName"]);
						teamDetails.TeamFlagImage = Convert.ToString(result["TeamFlagImage"]);
						teamDetails.TeamDesc = Convert.ToString(result["TeamDesc"]);
					}
				}
			}
            Home home = new Home();
          
            home.teamDetails = teamDetails;
            return View(home);
		}




		public IActionResult Contact()
    {
			 Home home = new Home();
			var teams = _dbContext.GetAllTeams();
			home.teams = teams;
			return View(home);
		}
    public IActionResult Fixtures()
    {
			Home home = new Home();
			var teams = _dbContext.GetAllTeams();
			home.teams = teams;
			return View(home);
		}
    public IActionResult Matches()
    {
        return View();
    }

	public IActionResult playersgallery()
	{
			Home home = new Home();
			var teams = _dbContext.GetAllTeams();
			var players = _dbContext.GetAllPalyer();
			home.teams = teams;
			home.players = players;
			return View(home);
		}

		public IActionResult AllTeamDetail(int teamId)
		{
			Home home = new Home();
			var players = _dbContext.GetAllPalyerbyTeam(teamId);
			var matchesbyid = _dbContext.GetAllMatchesbyteamid(teamId);
			home.matchresults = matchesbyid;
			home.players = players;


			return View(home);
		}

		[HttpPost]
		public JsonResult AjaxMethod(string pid)
		{
			Home home = new Home();
			var players = _dbContext.GetAllPalyerById(Convert.ToInt32(pid));

			
			JsonResult abc= Json(players);
			
			return abc;
		}




		public IActionResult Merchandise()
    {
        Home home = new Home();
        var teams = _dbContext.GetAllTeams();
        home.teams = teams;
        return View(home);

		}
		public IActionResult Matchresult()
		{
			Home home = new Home();
			var matchresults = _dbContext.GetAllMatches();
			home.matchresults = matchresults;
			return View(home);
		}
		public IActionResult UpcomingMatch()
		{
			return View();
		}
		public IActionResult Team()
		{
			return View();
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[HttpPost]
		public IActionResult Search(string searchtxt)
		{
			Home home = new Home(); var teamsall = _dbContext.GetAllTeams();

			var teams = _dbContext.GetAllTeamsbySearch().FindAll(x => x.teamname.Contains(searchtxt));
			var players = _dbContext.GetAllPalyerforSearch().FindAll(x => x.name.Contains(searchtxt));
			home.mysearch.teams = teams;
			home.mysearch.playerList = players;
			home.teams = teamsall;
			return View(home);




		}


	}
}