using Market.CheckoutComponent.Model.Interfaces;

namespace Market.WebApi.Services
{
	public interface IDataService
	{
		IProduct GetProductByName(string name);
	}
}
