using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Services.Interfaces;

namespace Market.WebApi.Services
{
	public class ProductBasketProviderService : IProductBasketProviderService
	{
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesProviderService discountRulesProviderService;

		private ProductsBasket current;

		public ProductBasketProviderService(ISalesHistoryService salesHistoryService, IDiscountRulesProviderService discountRulesProviderService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesProviderService = discountRulesProviderService;
		}

		public IProductsBasket GetCurrent()
		{
			return current ?? (current = new ProductsBasket(salesHistoryService, discountRulesProviderService));
		}

		public void Reset()
		{
			current = null;
		}
	}
}
