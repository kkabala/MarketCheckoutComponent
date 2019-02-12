using System.Collections.Generic;

namespace Market.CheckoutComponent.Model.Interfaces
{
	public interface IDiscountRule
	{
		string Name { get; }

		decimal Calculate(IEnumerable<IProduct> products);
	}
}