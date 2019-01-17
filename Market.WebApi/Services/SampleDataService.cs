using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.DiscountRules;
using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Services.Interfaces;

namespace Market.WebApi.Services
{
	public class SampleDataService : IDataService, IDiscountRulesProviderService
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

		public IDiscountRule[] GetAllDiscountRules()
		{
			return new IDiscountRule[]
			{
				new BulkDiscountRule("Bulk discount A", "A", 3, 70),
				new BulkDiscountRule("Bulk discount B", "B", 2, 15),
				new BulkDiscountRule("Bulk discount C", "C", 3, 60),
				new BulkDiscountRule("Bulk discount D", "D", 2, 40),

				new PackageDiscountRule("Package discount AB", -10, "A", "B"),
				new PackageDiscountRule("Package discount ABCD", -30, "A", "B", "C", "D"),

				new PriceThresholdDiscountRule("Sale", 600, 10)
			};
		}
	}
}