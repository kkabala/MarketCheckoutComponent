using Market.CheckoutComponent.Model.Interfaces;

namespace Market.WebApi.Services.Interfaces
{
	public interface IDataService
	{
		IProduct GetProductByName(string name);
	}
}