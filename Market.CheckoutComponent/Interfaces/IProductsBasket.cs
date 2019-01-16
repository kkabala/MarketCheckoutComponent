using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Interfaces
{
	public interface IProductsBasket
	{
		IBill Checkout();
		void Add(IProduct product);
		IProduct[] GetAll();
		void Remove(string productsName);
		void DecreaseUnits(string particularProductName);
	}
}