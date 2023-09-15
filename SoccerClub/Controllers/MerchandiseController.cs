using Microsoft.AspNetCore.Mvc;

namespace SoccerClub.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using SoccerClub.Models;
	
	

	public class MerchandiseController : Controller
	{
		private readonly ProductService _productService;
	
		private readonly ApplicationDbContext _dbContext;

		public MerchandiseController( ApplicationDbContext dbContext, ProductService productService)
		{
			
			_dbContext = dbContext;
			
			_productService = productService;
		}

		public IActionResult Index()
		{
			Home home = new Home();
			var Product = _dbContext.GetAllProducts();
			var teams = _dbContext.GetAllTeams();
			home.teams = teams;
			home.Products = Product;
			return View(home);
		}

		[HttpPost]
		public IActionResult AddToCart(int productId, int quantity)
		{
			var product = _dbContext.Products.Find(productId);


			var userId = HttpContext.Session.GetInt32("UserID");
			if (userId == null) userId = 0;
			var cartheader = _dbContext.cartheader.ToList();


			var cartHeader = _dbContext.cartheader.FirstOrDefault(c => c.User_Id == userId );
			if (cartHeader == null)
			{
				cartHeader = new CartHeader
				{
					User_Id = (int)userId,
					dated = DateTime.Now,
					status = 1
				};
				_dbContext.cartheader.Add(cartHeader);
				_dbContext.SaveChanges();
			}
		
			// Add the selected product to the cart with the specified quantity.
			var cartItem = new CartItem
			{
				ProductId = product.Id,
				HeaderId = cartHeader.Id,
				Quantity = quantity // Use the quantity specified by the user.
			};
			cartItem.hdid = cartItem.HeaderId;
			_dbContext.CartItems.Add(cartItem);
			_dbContext.SaveChanges();

			// Display a confirmation alert.
			TempData["Message"] = $"{quantity} {product.Name}(s) have been added to your cart.";

			// Redirect to the view cart action.
			return RedirectToAction("Index");
		}


	

		[HttpPost]
		public IActionResult RemoveFromCart(int cartItemId)
		{
			
			return RedirectToAction("ViewCart");
		}

		public IActionResult Checkout()
		{
			// Implement checkout logic here
			return View();
		}
	}

}
