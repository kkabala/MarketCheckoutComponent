using Market.CheckoutComponent.Interfaces;

namespace Market.Services.Interfaces
{
	public interface IProductBasketService
	{
		IProductsBasket GetCurrent();
		void Reset();
	}
}
