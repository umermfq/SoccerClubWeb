namespace SoccerClub.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public string username { get; set; }
        public DateTime dated { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }

        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
