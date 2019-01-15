using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Model.DiscountRules
{
	public class BulkDiscountRuleRule : IDiscountRule
	{
		private readonly string productName;
		private readonly int itemsRequiredToApplyDiscount;
		private readonly decimal specialGroupPrice;

		public BulkDiscountRuleRule(string name, string productName, int itemsRequiredToApplyDiscount, decimal specialGroupPrice)
		{
			Name = name;
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