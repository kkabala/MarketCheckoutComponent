using System.Collections.Generic;
using System.Linq;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.Interfaces;
using MarketCheckoutComponent.Services.Interfaces;

namespace MarketCheckoutComponent
{
	public class ProductsBasket
	{
		private readonly List<IProduct> products;
		private readonly ISalesHistoryService salesHistoryService;

		public ProductsBasket(ISalesHistoryService salesHistoryService)
		{
			this.salesHistoryService = salesHistoryService;
			products = new List<IProduct>();
		}

		public Bill Checkout()
		{
			var bill = new Bill(products.ToArray(), null);
			salesHistoryService.Add(bill);
			return bill;
		}

		public void AddProduct(IProduct product)
		{
			products.Add(product);
		}

		public IProduct[] GetAll()
		{
			return products.ToArray();
		}

		public void RemoveProducts(string productsName)
		{
			var productsToBeRemoved = new List<IProduct>();
			foreach (var product in products.Where(m=> m.Name == productsName))
				productsToBeRemoved.Add(product);

			foreach (var product in productsToBeRemoved)
				products.Remove(product);
		}

		public void DecreaseProductUnits(string particularProductName)
		{
			var productToBeRemoved = products.FirstOrDefault(m => m.Name == particularProductName);
			products.Remove(productToBeRemoved);
		}
	}
}