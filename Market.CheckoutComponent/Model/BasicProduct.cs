using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Model
{
	public class BasicProduct : IProduct
	{
		public BasicProduct(string name, decimal price)
		{
			Name = name;
			Price = price;
		}

		public string Name { get; }
		public decimal Price { get; }
	}
}