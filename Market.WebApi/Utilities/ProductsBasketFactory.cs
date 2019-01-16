using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Utilities.Interfaces;

namespace Market.WebApi.Utilities
{
	public class ProductsBasketFactory : IProductsBasketFactory
	{
		private readonly ISalesHistoryService salesHistoryService;

		public ProductsBasketFactory(ISalesHistoryService salesHistoryService)
		{
			this.salesHistoryService = salesHistoryService;
		}
		public IProductsBasket Create()
		{
			return new ProductsBasket(salesHistoryService);
		}
	}
}