using System;
using System.Collections.Generic;
using System.Linq;
using MarketCheckoutComponent.Model.Discounts.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.Discounts
{
	public class PackageDiscount : IDiscount
	{
		public string Name { get; }
		private readonly decimal discountAmount;
		private readonly string[] packageProductNames;

		public PackageDiscount(string name, decimal discountAmount, params string[] packageProductNames)
		{
			Name = name;
			this.discountAmount = discountAmount;
			this.packageProductNames = packageProductNames;
		}

		public decimal Calculate(IEnumerable<IProduct> products)
		{
			if (products != null)
			{
				var groupedProducts = products.GroupBy(m => m.Name).ToList();
				if (packageProductNames.All(m => groupedProducts.Any(p => p.Key == m)))
				{
					return discountAmount * groupedProducts.Select(m => m.Count()).Min();
				}
			}
			return 0;
		}
	}
}
