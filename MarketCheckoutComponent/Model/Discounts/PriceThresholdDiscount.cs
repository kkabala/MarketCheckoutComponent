using MarketCheckoutComponent.Model.Discounts.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MarketCheckoutComponent.Model.Discounts
{
	public class PriceThresholdDiscount : IDiscount
	{
		public string Name { get; }
		private readonly int priceThreshold;
		private readonly int discountPercentage;

		public PriceThresholdDiscount(string name, int priceThreshold, int discountPercentage)
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
