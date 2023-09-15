namespace SoccerClub.Models
{
	public class Home
	{
		public List<team> teams = new List<team>();
		public List<player> players = new List<player>();
		public List<matchresult> matchresults = new List<matchresult>();
		public List<login> logins = new List<login>();
		public List<Orders> Orders = new List<Orders>();
		public List<Product> Products = new List<Product>();
		public List<Register> Registers = new List<Register>();
	
		public Search mysearch = new Search();
        public team teamDetails { get; set; }
        public Orders OrderDetails { get; set; }
       

    }
}
