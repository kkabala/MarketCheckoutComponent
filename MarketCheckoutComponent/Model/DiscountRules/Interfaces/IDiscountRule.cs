using System.Collections.Generic;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.DiscountRules.Interfaces
{
	public interface IDiscountRule
	{
		string Name { get; }
		decimal Calculate(IEnumerable<IProduct> products);
	}
}