using Market.CheckoutComponent.Model.Interfaces;

namespace Market.Services.Interfaces
{
	public interface IDataService
	{
		IProduct GetProductByName(string name);
	}
}