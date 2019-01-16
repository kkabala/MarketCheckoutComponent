using Market.CheckoutComponent.Model.Interfaces;

namespace Market.WebApi.Model
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
