using System.Linq;
using System.Text;
using MarketCheckoutComponent.Model.Discounts.Interfaces;

namespace MarketCheckoutComponent.Model
{
	public class Bill
	{
		private const string headerSeparator = "     ";

		public Bill(Product[] products, IDiscount[] discounts)
		{
			Products = products ?? new Product[]{};
			Discounts = discounts ?? new IDiscount[]{};
		}

		public Product[] Products { get; private set; }
		public IDiscount[] Discounts { get; private set; }

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
			if (Discounts.Any())
			{
				outputBuilder.AppendLine();
				outputBuilder.AppendLine("Discounts applied:");

				foreach (var singleDiscount in Discounts)
				{
					outputBuilder.Append(singleDiscount.Name);
					outputBuilder.Append(": ");
					outputBuilder.AppendLine(singleDiscount.Calculate(Products).ToString("F2"));
				}
			}
		}
	}
}