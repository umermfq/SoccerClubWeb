namespace SoccerClub.Models
{
	public class matchresult
	{
		public int ID { get; set; }
		public int Team1_ID { get; set; }
		public int Team2_ID { get; set; }
		public DateTime MatchDatetime { get; set; }
		public int Team1_Goal { get; set; }
		public int Team2_Goal { get; set; }
		public string Team1Name { get; set; } // Added for convenience
		public string Team2Name { get; set; } // Added for convenience
		public string Team1FlagImage { get; set; } // Added for convenience
		public string Team2FlagImage { get; set; } // Added for convenience

        public int status { get; set; }
    }

}
