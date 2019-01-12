using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model
{
	public class Product : IProduct
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		public Product() { }

		public Product(string name, decimal price)
		{
			Name = name;
			Price = price;
		}
	}
}