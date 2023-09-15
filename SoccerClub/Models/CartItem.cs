namespace SoccerClub.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int HeaderId { get; set; }
		public int hdid { get; set; }
		public int Quantity { get; set; }
		public Product Product { get; set; }
	}

}
