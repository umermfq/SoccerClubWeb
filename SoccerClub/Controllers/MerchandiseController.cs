using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace SoccerClub.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.CodeAnalysis;
	using Microsoft.Data.SqlClient;
	using Microsoft.EntityFrameworkCore;

	using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

    using SoccerClub.Models;
	using System.Data;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
	using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Collections.Generic;
	using Microsoft.DotNet.MSIdentity.Shared;

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
			ViewBag.Message = TempData["Message"];
			return View(home);
		}

		[HttpPost]
		public IActionResult AddToCart(int productId, int quantity)
		{
			var product = _dbContext.Products.Find(productId);


			//var userId = HttpContext.Session.GetInt32("UserID");
			//if (userId == null) userId = 0;
			//var cartheader = _dbContext.cartheader.ToList();


			//var cartHeader = _dbContext.cartheader.FirstOrDefault(c => c.User_Id == userId );
			string? sessionCart= HttpContext.Session.GetString("Cart");
            List<cartdetail> cartdetail = null;


            if (sessionCart == null)
			{


               cartdetail = new List<cartdetail>();

               
            }
			else
			{
                cartdetail = new List<cartdetail>();
                JArray jsonResponse = JArray.Parse(sessionCart);



                foreach (var item in jsonResponse)
                {
                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString());
                    cartdetail objCart = new cartdetail();
                    objCart.name = item["name"].ToString();
                    objCart.Price = Convert.ToInt32(item["Price"]);
                    objCart.Quantity = Convert.ToInt32(item["Quantity"]);
					objCart.dated = Convert.ToDateTime(item["dated"]);
					objCart.Image = Convert.ToString(item["Image"]);
					objCart.ProductId = Convert.ToInt32(item["ProductId"]);
					objCart.Price = objCart.Price * objCart.Quantity;
                    //JObject jRaces = (JObject)item[0];
                    //foreach (var rItem in jRaces)
                    //{
                    //	string rItemKey = rItem.Key;
                    //	JObject rItemValueJson = (JObject)rItem.Value;
                    //	cartdetail rowsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<cartdetail>(rItemValueJson.ToString());
                    //}
                    cartdetail.Add(objCart);
                }
            }
			cartdetail cartdetails = new cartdetail
			{
				ProductId = product.Id,
                name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                Quantity = quantity,
                Status = 1,
                dated = DateTime.Now,
            };
			cartdetail findobj = cartdetail.Find(x => x.name == cartdetails.name);
			if (findobj == null)
			{
				cartdetail.Add(cartdetails);
			}
			else
			{
				findobj.Quantity += cartdetails.Quantity;
				findobj.Price += (cartdetails.Price*cartdetails.Quantity);
			}
			var jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(cartdetail);

            HttpContext.Session.SetString("Cart", jsontext);



            // Display a confirmation alert.
            TempData["Message"] = $"{quantity} {product.Name}(s) have been added to your cart.";

			// Redirect to the view cart action.
			return RedirectToAction("Index");
		 }

		public IActionResult ViewCart()
		{
			List<cartdetail> cartdetails = null;

			List<cartdetail> lst = new List<cartdetail>();
            string? sessionCart = HttpContext.Session.GetString("Cart");
            


            if (sessionCart == null)
            {


                cartdetails = new List<cartdetail>();


            }
            else
            {
				JArray jsonResponse = JArray.Parse(sessionCart);

			

				foreach (var item in jsonResponse)
				{
					var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString());
                    cartdetail objCart = new cartdetail();
					objCart.name = item["name"].ToString();
					objCart.Price = Convert.ToInt32(item["Price"]);
					objCart.Quantity = Convert.ToInt32(item["Quantity"]);
					objCart.dated = Convert.ToDateTime(item["dated"]);
					objCart.ProductId = Convert.ToInt32(item["ProductId"]);
					objCart.Image = Convert.ToString(item["Image"]);
					objCart.Price = objCart.Price * objCart.Quantity;
					//JObject jRaces = (JObject)item[0];
					//foreach (var rItem in jRaces)
					//{
					//	string rItemKey = rItem.Key;
					//	JObject rItemValueJson = (JObject)rItem.Value;
					//	cartdetail rowsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<cartdetail>(rItemValueJson.ToString());
					//}
					

					lst.Add(objCart);
                }
               

                //cartdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<cartdetail[]>(sessionCart);
            }
            Home home = new Home();

            home.cartdetails = lst;
			home.Total = lst.Sum(a => a.Price);
            return View(home);

        }




	
		public IActionResult RemoveFromCart(int productId)
		{
			string? sessionCart = HttpContext.Session.GetString("Cart");

			List<cartdetail> cartdetail = new List<cartdetail>();
			JArray jsonResponse = JArray.Parse(sessionCart);



			foreach (var item in jsonResponse)
			{
				var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString());
				cartdetail objCart = new cartdetail();
				objCart.name = item["name"].ToString();
				objCart.Price = Convert.ToInt32(item["Price"]);
				objCart.Quantity = Convert.ToInt32(item["Quantity"]);
				objCart.dated = Convert.ToDateTime(item["dated"]);
				objCart.Image = Convert.ToString(item["Image"]);
				objCart.ProductId = Convert.ToInt32(item["ProductId"]);
				objCart.Price = objCart.Price * objCart.Quantity;
				//JObject jRaces = (JObject)item[0];
				//foreach (var rItem in jRaces)
				//{
				//	string rItemKey = rItem.Key;
				//	JObject rItemValueJson = (JObject)rItem.Value;
				//	cartdetail rowsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<cartdetail>(rItemValueJson.ToString());
				//}
				if (objCart.ProductId != productId)
				{
					cartdetail.Add(objCart);
				}

			}
			var jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(cartdetail);

			HttpContext.Session.SetString("Cart", jsontext);

			//		lst.Remove("Mahesh Chand");
			return RedirectToAction("ViewCart");
		}

		public IActionResult Checkout()
		{
			// Implement checkout logic here
			return View();
		}
	}

}
