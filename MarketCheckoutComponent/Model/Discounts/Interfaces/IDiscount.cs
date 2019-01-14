using System.Collections.Generic;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.Discounts.Interfaces
{
	public interface IDiscount
	{
		string Name { get; }
		decimal Calculate(IEnumerable<IProduct> products);
	}
}