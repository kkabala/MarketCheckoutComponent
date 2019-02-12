using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Utilities.Interfaces;

namespace Market.Services.Utilities
{
	public class ProductsBasketFactory : IProductsBasketFactory
	{
		private readonly IDiscountRulesService discountRulesService;
		private readonly IProductDataService productDataService;
		private readonly ISalesHistoryService salesHistoryService;

		public ProductsBasketFactory(ISalesHistoryService salesHistoryService,
			IDiscountRulesService discountRulesService,
			IProductDataService productDataService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesService = discountRulesService;
			this.productDataService = productDataService;
		}

		public IProductsBasket Create()
		{
			return new ProductsBasket(salesHistoryService, discountRulesService, productDataService);
		}
	}
}