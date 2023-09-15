using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoccerClub.Models;

namespace SoccerClub.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            Home home = new Home();
            var Orders = _context.GetAllOrders();
            home.Orders = Orders;
            return View(Orders);
        }

        public IActionResult OrderDetails(string HeaderId)
        {
            var OrderDetails = new Orders(); // Replace "Team" with your actual entity class name
            var OrderDetail = new List<Orderdetail>(); // Replace "Team" with your actual entity class name

            var query = @"
                         select I.Id, P.Name,P.Description,P.Price,I.Quantity, I.Quantity*P.Price as Total from CartItems I 
inner join Products P on P.Id=I.ProductId where I.HeaderId=" + HeaderId.ToString();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                //command.Parameters.Add(new SqlParameter("@HeaderId", HeaderId));
                command.CommandType = System.Data.CommandType.Text;

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        OrderDetails.ID = Convert.ToInt32(result["ID"]);
                        OrderDetails.username = Convert.ToString(result["Name"]);
                        OrderDetails.Description = Convert.ToString(result["Description"]);
                        OrderDetails.Price = Convert.ToInt32(result["Price"]);
                        OrderDetails.Quantity = Convert.ToInt32(result["Quantity"]);
                        OrderDetails.Total = Convert.ToInt32(result["Total"]);
                        OrderDetails.Name = Convert.ToString(result["Name"]);
                    }
                }
            }
            Home home = new Home();

            home.OrderDetails = OrderDetails;
            return View(home);
        }

    }
}
