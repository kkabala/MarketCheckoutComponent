using System.Collections.Generic;
using MarketCheckoutComponent.Model;

namespace MarketCheckoutComponent
{
	public class ProductsBasket
	{
		private readonly List<Product> products;

		public ProductsBasket()
		{
			products = new List<Product>();
		}

		public Bill Checkout()
		{
			var bill = new Bill(products.ToArray(), null);
			return bill;
		}

		public void AddProduct(Product product)
		{
			products.Add(product);
		}
	}
}