using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Model.Interfaces;
using Market.WebApi.Model;

namespace Market.WebApi.Services
{
	public class SampleDataService : IDataService
	{
		private readonly List<IProduct> products = new List<IProduct>()
		{
			new BasicProduct("A", 40),
			new BasicProduct("B", 10),
			new BasicProduct("C", 30),
			new BasicProduct("D", 25),
		};

		public IProduct GetProductByName(string name)
		{
			return products.SingleOrDefault(m => m.Name == name);
		}
	}
}
