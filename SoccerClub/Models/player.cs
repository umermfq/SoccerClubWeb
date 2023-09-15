namespace SoccerClub.Models
{
	public class player
	{
        public int Id { get; set; }
        public string name { get; set; }
        public string teamname { get; set; }
        public int age { get; set; }
        public int Teamid { get; set; }
        public string   position { get; set; }
        public int  jersey { get; set; }
        public int  faulscommitted { get; set; }
		public int Goals { get; set; }
		public int ownGoals { get; set; }
		public int  redcards { get; set; }
        public int  yellowcards { get; set; }
        public int  offsides { get; set; }
        public int  totalplaytime { get; set; }
        public string   weight { get; set; }
        public double height { get; set; }

    }
}
