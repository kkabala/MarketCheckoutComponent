using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;

namespace Market.CheckoutComponent
{
	public class ProductsBasket : IProductsBasket
	{
		private readonly List<IProduct> products;
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesProviderService discountRulesProviderService;

		public ProductsBasket(ISalesHistoryService salesHistoryService,
			IDiscountRulesProviderService discountRulesProviderService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesProviderService = discountRulesProviderService;
			products = new List<IProduct>();
		}

		public IBill Checkout()
		{
			var bill = new Bill(products.ToArray(), discountRulesProviderService?.GetAllDiscountRules());
			salesHistoryService.Add(bill);
			return bill;
		}

		public void Add(IProduct product)
		{
			if (product!= null)
				products.Add(product);
		}

		public IProduct[] GetAll()
		{
			return products.ToArray();
		}

		public void Remove(string productsName)
		{
			var productsToBeRemoved = new List<IProduct>();
			foreach (var product in products.Where(m=> m.Name == productsName))
				productsToBeRemoved.Add(product);

			foreach (var product in productsToBeRemoved)
				products.Remove(product);
		}

		public void DecreaseUnits(string particularProductName)
		{
			var productToBeRemoved = products.FirstOrDefault(m => m.Name == particularProductName);
			products.Remove(productToBeRemoved);
		}
	}
}