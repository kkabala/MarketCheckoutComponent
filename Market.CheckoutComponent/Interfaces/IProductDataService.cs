using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Interfaces
{
	public interface IProductDataService
	{
		IProduct GetProductByName(string name);
	}
}