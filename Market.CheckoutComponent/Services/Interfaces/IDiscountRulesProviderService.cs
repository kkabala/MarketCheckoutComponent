using Market.CheckoutComponent.Model.DiscountRules.Interfaces;

namespace Market.CheckoutComponent.Services.Interfaces
{
	public interface IDiscountRulesProviderService
	{
		IDiscountRule[] GetAllDiscountRules();
	}
}
