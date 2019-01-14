using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.Discounts.Interfaces
{
	public interface IDiscount
	{
		string Name { get; }
		decimal Calculate(IProduct[] products);
	}
}