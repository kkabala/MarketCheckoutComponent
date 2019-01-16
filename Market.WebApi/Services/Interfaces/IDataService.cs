using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.WebApi.Services
{
	public interface IDataService
	{
		IProduct GetProductByName(string name);
	}
}
