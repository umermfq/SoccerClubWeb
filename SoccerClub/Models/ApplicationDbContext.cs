using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

namespace SoccerClub.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<CartHeader> cartheader { get; set; }
		public DbSet<player> player { get; set; }
		public DbSet<team> team { get; set; }
		public DbSet<matchresult> matches { get; set; }
		public DbSet<login> login { get; set; }
		public DbSet<Orders> order { get; set; }
		public DbSet<Orderdetail> Orderdetails { get; set; }

		public DbSet<Register> Register { get; set; }
		// Other DbSet properties

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{


		}


     
        public List<Orders> GetAllOrders()
        {
            var order = new List<Orders>();

            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select top 10 H.ID, L.username,H.dated,S.Name as StatusName,Sum(P.Price*I.Quantity) as Total  from cartheader H inner join CartItems I on I.HeaderId=H.Id\r\ninner join Products P on P.Id=I.ProductId inner join [Login] L on L.ID=H.User_Id inner join orderstatus S on S.ID=H.status group by H.ID,L.username,H.dated,S.Name order by H.Dated Desc ";
                command.CommandType = System.Data.CommandType.Text;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Orders o = new Orders();
                        o.ID= Convert.ToInt32(result[0]);
                        o.username = Convert.ToString(result[1]);
                        o.dated = Convert.ToDateTime(result[2]);
                        o.StatusName = Convert.ToString(result[3]);
                        o.Total = Convert.ToInt32(result[4]);



                        order.Add(o);
                    }
                }
            }

            return order;
        }
        public List<Product> GetAllProducts()
        {
            var product = new List<Product>();

            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * from Products ";
                command.CommandType = System.Data.CommandType.Text;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Product p = new Product();
                        p.Id = Convert.ToInt32(result[0]);
                        p.Name = Convert.ToString(result[1]);
                        p.Description = Convert.ToString(result[2]);
                        p.Price = Convert.ToInt32(result[3]);
                        p.Image = Convert.ToString(result[4]);



                        product.Add(p);
                    }
                }
            }

            return product;
        }
        public List<Register> GetAllUsers()
        {
            var user = new List<Register>();

            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * from Register ";
                command.CommandType = System.Data.CommandType.Text;

                Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Register re = new Register();
                        re.ID = Convert.ToInt32(result[0]);
                        re.FirstName = Convert.ToString(result[1]);
                        re.LastName = Convert.ToString(result[2]);
                        re.Email = Convert.ToString(result[4]);
                        re.PhoneNumber = Convert.ToString(result[5]);
                        re.Gender = Convert.ToString(result[7]);
                        re.isAdmin = Convert.ToInt32(result[8]);
                       


                        user.Add(re);
                    }
                }
            }

            return user;
        }
        public List<matchresult> GetAllMatches()
		{
			var MatchesName = new List<matchresult>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "SELECT  m.ID, t1.TeamName AS Team1Name,  t2.TeamName AS Team2Name,  m.MatchDatetime, m.Team1_Goal AS HomeGoal, m.Team2_Goal AS AwayGoal, t1.TeamFlagImage AS Team1FlagImage,  t2.TeamFlagImage AS Team2FlagImage FROM Matches m INNER JOIN Team t1 ON m.Team1_ID = t1.ID INNER JOIN Team t2 ON m.Team2_ID = t2.ID";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						matchresult mr = new matchresult();
						mr.ID = Convert.ToInt32(result[0]);
						mr.Team1Name = Convert.ToString(result[1]);
						mr.Team2Name = Convert.ToString(result[2]);
						mr.MatchDatetime = Convert.ToDateTime(result[3]);
						mr.Team1_Goal = Convert.ToInt32(result[4]);
						mr.Team2_Goal = Convert.ToInt32(result[5]);
						mr.Team1FlagImage = Convert.ToString(result[6]);
						mr.Team2FlagImage = Convert.ToString(result[7]);
						

						MatchesName.Add(mr);
					}
				}
			}

			return MatchesName;
		}



		public List<matchresult> GetAllMatchesbyteamid(int teamid)
		{
			var MatchesName = new List<matchresult>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "SELECT  m.ID, t1.TeamName AS Team1Name,  t2.TeamName AS Team2Name,  m.MatchDatetime, m.Team1_Goal AS HomeGoal, m.Team2_Goal AS AwayGoal, t1.TeamFlagImage AS Team1FlagImage,  t2.TeamFlagImage AS Team2FlagImage, t1.id,t2.id FROM Matches m INNER JOIN Team t1 ON m.Team1_ID = t1.ID INNER JOIN Team t2 ON m.Team2_ID = t2.ID where Team1_ID = " + teamid + " or Team2_ID = " + teamid + "";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						matchresult mr = new matchresult();
						mr.ID = Convert.ToInt32(result[0]);
						mr.Team1Name = Convert.ToString(result[1]);
						mr.Team2Name = Convert.ToString(result[2]);
						mr.MatchDatetime = Convert.ToDateTime(result[3]);
						mr.Team1_Goal = Convert.ToInt32(result[4]);
						mr.Team2_Goal = Convert.ToInt32(result[5]);
						mr.Team1FlagImage = Convert.ToString(result[6]);
						mr.Team2FlagImage = Convert.ToString(result[7]);
						mr.Team1_ID = Convert.ToInt32(result[8]);
						mr.Team2_ID = Convert.ToInt32(result[9]);


						MatchesName.Add(mr);
					}
				}
			}
			foreach(var item in  MatchesName)
			{
				if(item.Team1_ID != teamid)
				{
					
						var team1ID = item.Team1_ID;
						var team1Name = item.Team1Name;
						var team1icon = item.Team1FlagImage;
						var team1goal = item.Team1_Goal;

					item.Team1_ID = item.Team2_ID;
					item.Team1Name = item.Team2Name;
					item.Team1FlagImage = item.Team2FlagImage;
					item.Team1_Goal = item.Team2_Goal;

					item.Team2_ID = team1ID;
					item.Team2Name = team1Name;
					item.Team2FlagImage = team1icon;
					item.Team2_Goal = team1goal;


				}
				if(item.Team1_ID == teamid)
				{
					if (item.Team1_Goal > item.Team2_Goal) item.status = 1;
					else if (item.Team1_Goal == item.Team2_Goal) item.status = 2;
					else item.status = 0;
				}
				
			}


			return MatchesName;
		}

		public List<player> GetAllPalyer()
		{
			var playerNames = new List<player>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = " SELECT top 10 p.Id, p.name, t.teamname, p.age, ps.OwnGoals FROM player p INNER JOIN Team t ON p.TeamID = t.ID INNER JOIN playerstatistics ps ON p.ID = ps.Playerid";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						player p = new player();
						p.Id = Convert.ToInt32(result[0]);
						p.name = Convert.ToString(result[1]);
						p.teamname = Convert.ToString(result[2]);
						p.age = Convert.ToInt32(result[3]);
						p.Goals = Convert.ToInt32(result[4]);

						playerNames.Add(p);
					}
				}
			}

			return playerNames;
		}

		public List<player> GetAllPalyerforSearch()
		{
			var playerNames = new List<player>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = " SELECT  p.Id, p.name, t.teamname, p.age, ps.OwnGoals FROM player p INNER JOIN Team t ON p.TeamID = t.ID INNER JOIN playerstatistics ps ON p.ID = ps.Playerid";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						player p = new player();
						p.Id = Convert.ToInt32(result[0]);
						p.name = Convert.ToString(result[1]);
						p.teamname = Convert.ToString(result[2]);
						p.age = Convert.ToInt32(result[3]);
						p.Goals = Convert.ToInt32(result[4]);

						playerNames.Add(p);
					}
				}
			}

			return playerNames;
		}


		public player GetAllPalyerById(int pid)
		{
			player p = new player();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = " select * from Player P inner join PlayerStatistics  PS on PS.Playerid=P.ID where P.ID = "+ pid + "";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						
						p.Id = Convert.ToInt32(result[0]);
						p.name = Convert.ToString(result[1]);
						p.weight = Convert.ToString(result[2]);
						p.height = Convert.ToDouble(result[3]);
						p.age = Convert.ToInt32(result[4]);
						p.jersey = Convert.ToInt32(result[5]);
						p.position = Convert.ToString(result[6]);
						p.Teamid = Convert.ToInt32(result[7]);
						p.totalplaytime = Convert.ToInt32(result[12]);
						p.faulscommitted = Convert.ToInt32(result[13]);
						p.ownGoals = Convert.ToInt32(result[14]);
						p.offsides = Convert.ToInt32(result[15]);
						p.redcards = Convert.ToInt32(result[16]);
						p.yellowcards = Convert.ToInt32(result[17]);

						
					}
				}
			}

			return p;
		}
		public List<player> GetAllPalyerbyTeam(int teamid)
		{
			var playerNames = new List<player>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "select * from Player where TeamID = " + teamid + " order by Position";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						player p = new player();
						p.Id = Convert.ToInt32(result[0]);
						p.name = Convert.ToString(result[1]);
						p.age = Convert.ToInt32(result[4]);
						p.Teamid = Convert.ToInt32(result[7]);
						p.position = Convert.ToString(result[6]);

						playerNames.Add(p);
					}
				}
			}

			return playerNames;
		}
		public List<team> GetAllTeams ()
		{
			var teamNames = new List<team>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "select ID, TeamName  from team ";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						team t = new team();
						t.Id = Convert.ToInt32(result[0]);
						t.teamname = Convert.ToString(result[1]);
					
						teamNames.Add(t);
					}
				}
			}

			return teamNames;
		}



		public List<team> GetAllTeamsbySearch()
		{
			var teamNames = new List<team>();

			using (var command = Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "select t.id,l.Name as LeagueName,t.TeamName,t.TeamFlagImage,t.TeamDesc from Team t inner join Season s on s.ID = t.SeasonID inner join League l on l.ID = s.LeagueID ";
				command.CommandType = System.Data.CommandType.Text;

				Database.OpenConnection();

				using (var result = command.ExecuteReader())
				{
					while (result.Read())
					{
						team t = new team();
						t.Id = Convert.ToInt32(result[0]);
						t.Leaguename = Convert.ToString(result[1]);
						t.teamname = Convert.ToString(result[2]);
						t.TeamFlagImage = Convert.ToString(result[3]);
						t.TeamDesc = Convert.ToString(result[4]);

						teamNames.Add(t);
					}
				}
			}

			return teamNames;
		}
	}
}
