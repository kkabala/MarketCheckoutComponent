using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Interfaces
{
	public interface IProductsBasket
	{
		IBill Checkout();
		void Add(string productName);
		IProduct[] GetAllAdded();
		void Remove(string productsName);
		void DecreaseUnits(string particularProductName);
	}
}