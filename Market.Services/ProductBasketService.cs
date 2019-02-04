using Market.CheckoutComponent.Interfaces;
using Market.Services.Interfaces;
using Market.Services.Utilities.Interfaces;

namespace Market.Services
{
	public class ProductBasketService : IProductBasketService
	{
		private readonly IProductsBasketFactory productsBasketFactory;

		private IProductsBasket current;

		public ProductBasketService(IProductsBasketFactory productsBasketFactory)
		{
			this.productsBasketFactory = productsBasketFactory;
		}

		public IProductsBasket GetCurrent()
		{
			return current ?? (current = productsBasketFactory.Create());
		}

		public void Reset()
		{
			current = null;
		}
	}
}
