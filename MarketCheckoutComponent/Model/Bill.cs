using System;
using System.Linq;
using System.Text;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model
{
	public class Bill
	{
		private const string ProductHeader = "Product";
		private const string PriceHeader = "Price";
		private const string UnitHeader = "Unit";

		private const string ProductColumnFormatter = "{0, -15}";
		private const string PriceColumnFormatter = "{0, -8}";
		private const string UnitColumnFormatter = "{0, 5}";

		public Bill(IProduct[] products, IDiscountRule[] discountsRule)
		{
			Products = products ?? new IProduct[]{};
			DiscountsRule = discountsRule ?? new IDiscountRule[]{};
		}

		public IProduct[] Products { get; }
		public IDiscountRule[] DiscountsRule { get; }

		public override string ToString()
		{
			StringBuilder outputBuilder = new StringBuilder();
			ApplyProductsHeader(outputBuilder);
			ApplyProductsInfo(outputBuilder);
			ApplyDiscountsInfo(outputBuilder);
			return outputBuilder.ToString();
		}

		private void ApplyProductsHeader(StringBuilder outputBuilder)
		{
			outputBuilder.Append(String.Format(ProductColumnFormatter, ProductHeader));
			outputBuilder.Append(String.Format(PriceColumnFormatter, PriceHeader));
			outputBuilder.Append(String.Format(UnitColumnFormatter, UnitHeader));
			outputBuilder.AppendLine();
			var headerLength = outputBuilder.Length;
			outputBuilder.Append('-', headerLength);

		}
		private void ApplyProductsInfo(StringBuilder outputBuilder)
		{
			foreach (var singleProductGroup in Products.GroupBy(m => m.Name))
			{
				outputBuilder.AppendLine();
				outputBuilder.Append(String.Format(ProductColumnFormatter, singleProductGroup.Key));
				outputBuilder.Append(String.Format(PriceColumnFormatter, singleProductGroup.First().Price.ToString("F2")));
				outputBuilder.Append(String.Format(UnitColumnFormatter, singleProductGroup.Count()));
			}
		}

		private void ApplyDiscountsInfo(StringBuilder outputBuilder)
		{
			if (DiscountsRule.Any())
			{
				outputBuilder.AppendLine();
				outputBuilder.AppendLine("Discounts applied:");

				foreach (var singleDiscount in DiscountsRule)
				{
					outputBuilder.Append(singleDiscount.Name);
					outputBuilder.Append(": ");
					outputBuilder.AppendLine(singleDiscount.Calculate(Products).ToString("F2"));
				}
			}
		}
	}
}