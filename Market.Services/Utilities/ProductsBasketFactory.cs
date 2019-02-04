using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Utilities.Interfaces;

namespace Market.Services.Utilities
{
	public class ProductsBasketFactory : IProductsBasketFactory
	{
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesService discountRulesService;

		public ProductsBasketFactory(ISalesHistoryService salesHistoryService,
			IDiscountRulesService discountRulesService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesService = discountRulesService;
		}
		public IProductsBasket Create()
		{
			return new ProductsBasket(salesHistoryService, discountRulesService);
		}
	}
}