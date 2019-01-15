using System;
using System.Linq;
using System.Text;
using Market.CheckoutComponent.Model.DiscountRules.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Model
{
	public class Bill
	{
		private const string ProductHeader = "Product";
		private const string PriceHeader = "Price";
		private const string UnitHeader = "Unit";
		private const string AmountHeader = "Amount";

		private const string ProductColumnFormatter = "{0, -15}";
		private const string PriceColumnFormatter = "{0, -8}";
		private const string UnitColumnFormatter = "{0, 5}";
		private const string AmountColumnFormatter = "{0, 8}";

		public Bill(IProduct[] products, IDiscountRule[] discountsRule)
		{
			Products = products ?? new IProduct[] { };
			DiscountsRule = discountsRule ?? new IDiscountRule[] { };
		}

		public IProduct[] Products { get; }
		public IDiscountRule[] DiscountsRule { get; }

		public decimal Total
		{
			get
			{
				var productsSum = Products.Sum(m => m.Price);
				foreach (var discount in DiscountsRule)
				{
					productsSum += discount.Calculate(Products);
				}

				return productsSum;
			}
		}

		public override string ToString()
		{
			StringBuilder outputBuilder = new StringBuilder();
			ApplyProductsHeader(outputBuilder);
			ApplyProductsInfo(outputBuilder);
			ApplyDiscountsInfo(outputBuilder);
			ApplyTotalInfo(outputBuilder);
			return outputBuilder.ToString();
		}

		private void ApplyTotalInfo(StringBuilder outputBuilder)
		{
			outputBuilder.AppendLine();
			outputBuilder.Append($"Total: {Total}");
		}

		private void ApplyProductsHeader(StringBuilder outputBuilder)
		{
			outputBuilder.Append(String.Format(ProductColumnFormatter, ProductHeader));
			outputBuilder.Append(String.Format(PriceColumnFormatter, PriceHeader));
			outputBuilder.Append(String.Format(UnitColumnFormatter, UnitHeader));
			outputBuilder.Append(String.Format(AmountColumnFormatter, AmountHeader));
			outputBuilder.AppendLine();
			var headerLength = outputBuilder.Length;
			outputBuilder.Append('-', headerLength);
		}

		private void ApplyProductsInfo(StringBuilder outputBuilder)
		{
			foreach (var singleProductGroup in Products.GroupBy(m => m.Name))
			{
				outputBuilder.AppendLine();
				var productPrice = singleProductGroup.First().Price;
				var units = singleProductGroup.Count();
				outputBuilder.Append(String.Format(ProductColumnFormatter, singleProductGroup.Key));
				outputBuilder.Append(String.Format(PriceColumnFormatter, productPrice.ToString("F2")));
				outputBuilder.Append(String.Format(UnitColumnFormatter, units));
				outputBuilder.Append(String.Format(AmountColumnFormatter, (units * productPrice).ToString("F2")));
			}
			outputBuilder.AppendLine();
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