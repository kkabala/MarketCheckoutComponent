using System.Collections.Generic;
using System.Linq;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.DiscountRules
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
			if (products == null)
			{
				return 0;
			}
			var productsSum = products.Sum(m => m.Price);
			return productsSum >= priceThreshold ? -(discountPercentage * productsSum) / 100 : 0;
		}
	}
}