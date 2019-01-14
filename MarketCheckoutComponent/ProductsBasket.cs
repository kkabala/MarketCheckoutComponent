using System.Collections.Generic;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent
{
	public class ProductsBasket
	{
		private readonly List<IProduct> products;

		public ProductsBasket()
		{
			products = new List<IProduct>();
		}

		public Bill Checkout()
		{
			var bill = new Bill(products.ToArray(), null);
			return bill;
		}

		public void AddProduct(IProduct product)
		{
			products.Add(product);
		}
	}
}