namespace Market.CheckoutComponent.Model.Interfaces
{
	public interface IBill
	{
		IProduct[] Products { get; }
		IDiscountRule[] DiscountsRules { get; }
		decimal Total { get; }
	}
}