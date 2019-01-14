using System.Linq;
using System.Text;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;

namespace MarketCheckoutComponent.Model
{
	public class Bill
	{
		private const string headerSeparator = "     ";

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
			outputBuilder.Append("Product");
			outputBuilder.Append(headerSeparator);
			outputBuilder.Append("Price");
			outputBuilder.Append(headerSeparator);
			outputBuilder.Append("Unit");
			outputBuilder.Append(headerSeparator);
			outputBuilder.Append("Special Price");
			var headerLength = outputBuilder.Length;
			outputBuilder.Append('-', headerLength);

		}
		private void ApplyProductsInfo(StringBuilder outputBuilder)
		{
			foreach (var singleProductGroup in Products.GroupBy(m => m.Name))
			{
				outputBuilder.AppendLine();
				outputBuilder.Append(singleProductGroup.Key);
				outputBuilder.Append(headerSeparator);
				outputBuilder.Append(singleProductGroup.First().Price.ToString("F2"));
				outputBuilder.Append(headerSeparator);
				outputBuilder.Append(singleProductGroup.Count());
				outputBuilder.Append(headerSeparator);
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