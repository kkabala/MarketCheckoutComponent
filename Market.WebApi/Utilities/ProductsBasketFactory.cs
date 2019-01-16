using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Utilities.Interfaces;

namespace Market.WebApi.Utilities
{
	public class ProductsBasketFactory : IProductsBasketFactory
	{
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesProviderService discountRulesProviderService;

		public ProductsBasketFactory(ISalesHistoryService salesHistoryService,
			IDiscountRulesProviderService discountRulesProviderService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesProviderService = discountRulesProviderService;
		}
		public IProductsBasket Create()
		{
			return new ProductsBasket(salesHistoryService, discountRulesProviderService);
		}
	}
}