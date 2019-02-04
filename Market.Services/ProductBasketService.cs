using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Interfaces;

namespace Market.Services
{
	public class ProductBasketService : IProductBasketService
	{
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesService discountRulesService;

		private ProductsBasket current;

		public ProductBasketService(ISalesHistoryService salesHistoryService, IDiscountRulesService discountRulesService)
		{
			this.salesHistoryService = salesHistoryService;
			this.discountRulesService = discountRulesService;
		}

		public IProductsBasket GetCurrent()
		{
			return current ?? (current = new ProductsBasket(salesHistoryService, discountRulesService));
		}

		public void Reset()
		{
			current = null;
		}
	}
}
