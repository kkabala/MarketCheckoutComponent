using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Utilities.Interfaces;

namespace Market.WebApi.Utilities
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