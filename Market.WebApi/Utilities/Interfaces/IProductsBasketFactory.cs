using Market.CheckoutComponent.Interfaces;

namespace Market.WebApi.Utilities.Interfaces
{
	public interface IProductsBasketFactory
	{
		IProductsBasket Create();
	}
}