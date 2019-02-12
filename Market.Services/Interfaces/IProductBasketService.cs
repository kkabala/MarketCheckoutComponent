using Market.CheckoutComponent.Interfaces;

namespace Market.Services.Interfaces
{
	public interface IProductBasketService
	{
		void AddProduct(string productName);
		string Checkout();
		void DecreaseUnits(string productName);
	}
}