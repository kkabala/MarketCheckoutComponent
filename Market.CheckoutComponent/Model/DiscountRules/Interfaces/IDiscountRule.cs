using System.Collections.Generic;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Model.DiscountRules.Interfaces
{
	public interface IDiscountRule
	{
		string Name { get; }
		decimal Calculate(IEnumerable<IProduct> products);
	}
}