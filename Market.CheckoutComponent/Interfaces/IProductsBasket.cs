using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Interfaces
{
	public interface IProductsBasket
	{
		void Add(string productName);

		IBill Checkout();

		void DecreaseUnits(string particularProductName);

		IProduct[] GetAllAdded();

		void Remove(string productsName);
	}
}