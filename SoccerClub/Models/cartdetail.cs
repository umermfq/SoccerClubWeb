namespace SoccerClub.Models
{
	public class cartdetail
	{
        public int Id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public DateTime dated { get; set; }
    }
}
