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

		private IProductsBasket GetCurrent()
		{
			return current ?? (current = productsBasketFactory.Create());
		}

		private void Reset()
		{
			current = null;
		}

		public void AddProduct(string productName)
		{
			GetCurrent().Add(productName);
		}

		public string Checkout()
		{
			var bill = GetCurrent().Checkout();
			Reset();
			return bill.ToString();
		}

		public void DecreaseUnits(string productName)
		{
			GetCurrent().DecreaseUnits(productName);
		}
	}
}