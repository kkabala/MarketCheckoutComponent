namespace Market.CheckoutComponent.Model.Interfaces
{
	public interface IBill
	{
		IDiscountRule[] DiscountsRules { get; }
		IProduct[] Products { get; }
		decimal Total { get; }
	}
}