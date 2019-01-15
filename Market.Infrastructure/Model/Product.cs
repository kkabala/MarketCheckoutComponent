using System.ComponentModel.DataAnnotations;
using MarketCheckoutComponent.Model.Interfaces;

namespace Market.Infrastructure.Model
{
	public class Product : IProduct
	{
		[Key]
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