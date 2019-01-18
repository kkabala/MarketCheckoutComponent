using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Services.Interfaces
{
	public interface IDiscountRulesProviderService
	{
		IDiscountRule[] GetAllDiscountRules();
	}
}
