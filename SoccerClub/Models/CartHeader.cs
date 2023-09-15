namespace SoccerClub.Models
{
	public class CartHeader
	{

		public int Id { get; set; }
		public int User_Id { get; set; }
		public DateTime dated { get; set; }
		public int status { get; set; }
		public List<CartItem> CartItems { get; set; } 

	}
}
