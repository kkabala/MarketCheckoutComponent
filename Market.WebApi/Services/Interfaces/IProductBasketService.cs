using Market.CheckoutComponent.Interfaces;

namespace Market.WebApi.Services.Interfaces
{
	public interface IProductBasketService
	{
		IProductsBasket GetCurrent();
		void Reset();
	}
}
