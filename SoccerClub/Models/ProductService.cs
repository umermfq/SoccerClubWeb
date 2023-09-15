namespace SoccerClub.Models
{
	public class ProductService
	{
		private readonly ApplicationDbContext _dbContext;

		public ProductService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<Product> GetAllProducts()
		{
			return _dbContext.Products.ToList();
		}

		public Product GetProductById(int productId)
		{
			return _dbContext.Products.FirstOrDefault(p => p.Id == productId);
		}
	}
}
