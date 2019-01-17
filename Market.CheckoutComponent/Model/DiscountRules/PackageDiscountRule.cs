using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Market.CheckoutComponent.Model.DiscountRules
{
	public class PackageDiscountRule : IDiscountRule
	{
		public string Name { get; }
		private readonly decimal discountAmount;
		private readonly string[] packageProductNames;

		public PackageDiscountRule(string name, decimal discountAmount, params string[] packageProductNames)
		{
			Name = name;
			this.discountAmount = discountAmount;
			this.packageProductNames = packageProductNames;
		}

		public decimal Calculate(IEnumerable<IProduct> products)
		{
			var groupedProducts = products?.GroupBy(m => m.Name).ToList();

			return (groupedProducts != null 
				&& packageProductNames.All(m => groupedProducts.Any(p => p.Key == m))) 
				? discountAmount * groupedProducts.Select(m => m.Count()).Min() 
				 : 0;
		}
	}
}