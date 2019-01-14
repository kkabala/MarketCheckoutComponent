using System;
using System.Linq;
using System.Text;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model
{
	public class Bill
	{
		private const string productHeader = "Product";
		private const string priceHeader = "Price";
		private const string unitHeader = "Unit";

		private const string productColumnFormatter = "{0, -15}";
		private const string priceColumnFormatter = "{0, -8}";
		private const string unitColumnFormatter = "{0, 5}";

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
			outputBuilder.Append(String.Format(productColumnFormatter, productHeader));
			outputBuilder.Append(String.Format(priceColumnFormatter, priceHeader));
			outputBuilder.Append(String.Format(unitColumnFormatter, unitHeader));
			outputBuilder.AppendLine();
			var headerLength = outputBuilder.Length;
			outputBuilder.Append('-', headerLength);

		}
		private void ApplyProductsInfo(StringBuilder outputBuilder)
		{
			foreach (var singleProductGroup in Products.GroupBy(m => m.Name))
			{
				outputBuilder.AppendLine();
				outputBuilder.Append(String.Format(productColumnFormatter, singleProductGroup.Key));
				outputBuilder.Append(String.Format(priceColumnFormatter, singleProductGroup.First().Price.ToString("F2")));
				outputBuilder.Append(String.Format(unitColumnFormatter, singleProductGroup.Count()));
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