using Market.CheckoutComponent.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Market.CheckoutComponent.Model.DiscountRules
{
	public class PriceThresholdDiscountRule : IDiscountRule
	{
		private readonly int discountPercentage;
		private readonly int priceThreshold;

		public PriceThresholdDiscountRule(string name, int priceThreshold, int discountPercentage)
		{
			Name = name;
			this.priceThreshold = priceThreshold;
			this.discountPercentage = discountPercentage;
		}

		public string Name { get; }

		public decimal Calculate(IEnumerable<IProduct> products)
		{
			var productsSum = products?.Sum(m => m.Price);

			return (productsSum.HasValue && productsSum.Value >= priceThreshold)
				? -(discountPercentage * productsSum.Value) / 100
				: 0;
		}
	}
}