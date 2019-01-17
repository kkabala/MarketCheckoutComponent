using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Model.DiscountRules
{
	public class PriceThresholdDiscountRule : IDiscountRule
	{
		public string Name { get; }
		private readonly int priceThreshold;
		private readonly int discountPercentage;

		public PriceThresholdDiscountRule(string name, int priceThreshold, int discountPercentage)
		{
			Name = name;
			this.priceThreshold = priceThreshold;
			this.discountPercentage = discountPercentage;
		}

		public decimal Calculate(IEnumerable<IProduct> products)
		{
			var productsSum = products?.Sum(m => m.Price);

			return (productsSum.HasValue && productsSum.Value >= priceThreshold) 
				? -(discountPercentage * productsSum.Value) / 100 
				: 0;
		}
	}
}