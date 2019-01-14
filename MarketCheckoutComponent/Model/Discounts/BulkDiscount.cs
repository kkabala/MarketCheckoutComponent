using System.Collections.Generic;
using System.Linq;
using MarketCheckoutComponent.Model.Discounts.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model.Discounts
{
	public class BulkDiscount : IDiscount
	{
		private readonly string productName;
		private readonly int itemsRequiredToApplyDiscount;
		private readonly decimal specialGroupPrice;

		public BulkDiscount(string discountName, string productName, int itemsRequiredToApplyDiscount, decimal specialGroupPrice)
		{
			Name = discountName;
			this.productName = productName;
			this.itemsRequiredToApplyDiscount = itemsRequiredToApplyDiscount;
			this.specialGroupPrice = specialGroupPrice;
		}

		public string Name { get; }

		public decimal Calculate(IEnumerable<IProduct> products)
		{
			if (products == null)
			{
				return 0;
			}

			var discountedProducts = products.Where(m => m.Name == productName).ToList();

			if (discountedProducts.Count < itemsRequiredToApplyDiscount)
			{
				return 0;
			}

			var numberOfDiscounts = discountedProducts.Count / itemsRequiredToApplyDiscount;

			var discountPricesProductsSum = numberOfDiscounts * specialGroupPrice;
			var regularPriceSum = discountedProducts.Count * discountedProducts.First().Price;

			return discountPricesProductsSum - regularPriceSum;
		}
	}
}