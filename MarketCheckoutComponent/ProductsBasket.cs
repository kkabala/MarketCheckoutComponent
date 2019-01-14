using System.Collections.Generic;
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
	}
}