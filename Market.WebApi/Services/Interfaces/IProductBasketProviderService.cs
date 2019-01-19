using Market.CheckoutComponent.Interfaces;

namespace Market.WebApi.Services.Interfaces
{
	public interface IProductBasketProviderService
	{
		IProductsBasket GetCurrent();
		void Reset();
	}
}
