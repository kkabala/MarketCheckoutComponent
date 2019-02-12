using Market.CheckoutComponent.Model.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace Market.CheckoutComponent.Model
{
	internal class Bill : IBill
	{
		private const string AmountColumnFormatter = "{0, 8}";
		private const string AmountHeader = "Amount";
		private const string PriceColumnFormatter = "{0, -8}";
		private const string PriceHeader = "Price";
		private const string ProductColumnFormatter = "{0, -15}";
		private const string ProductHeader = "BasicProduct";
		private const string UnitColumnFormatter = "{0, 5}";
		private const string UnitHeader = "Unit";

		public Bill(IProduct[] products, IDiscountRule[] discountsRule)
		{
			Products = products ?? new IProduct[] { };
			DiscountsRules = discountsRule ?? new IDiscountRule[] { };
		}

		public IDiscountRule[] DiscountsRules { get; }
		public IProduct[] Products { get; }

		public decimal Total
		{
			get
			{
				return Products.Sum(m => m.Price) + DiscountsRules.Sum(discount => discount.Calculate(Products));
			}
		}

		public override string ToString()
		{
			var outputBuilder = new StringBuilder();
			ApplyProductsHeader(outputBuilder);
			ApplyProductsInfo(outputBuilder);
			ApplyDiscountsInfo(outputBuilder);
			ApplyTotalInfo(outputBuilder);
			return outputBuilder.ToString();
		}

		private void ApplyDiscountsInfo(StringBuilder outputBuilder)
		{
			if (!DiscountsRules.Any())
			{
				return;
			}
			outputBuilder.AppendLine();
			outputBuilder.AppendLine("Discounts applied:");

			foreach (var singleDiscount in DiscountsRules)
			{
				var discountValue = singleDiscount.Calculate(Products);
				if (discountValue >= 0)
				{
					continue;
				}
				outputBuilder.Append(singleDiscount.Name);
				outputBuilder.Append(": ");
				outputBuilder.AppendLine(discountValue.ToString("F2"));
			}
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

		private void ApplyTotalInfo(StringBuilder outputBuilder)
		{
			outputBuilder.AppendLine();
			outputBuilder.Append($"Total: {Total}");
		}
	}
}