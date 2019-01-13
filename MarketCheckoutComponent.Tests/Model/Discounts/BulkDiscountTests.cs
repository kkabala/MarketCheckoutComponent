using System.Collections.Generic;
using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.Discounts;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests.Model.Discounts
{
	[TestFixture]
	public class BulkDiscountTests
	{
		[TestCase("Christmas discount", "Product1", 3, 70, 40)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20)]
		[TestCase("Sale discount", "Product3", 2, 80, 50)]
		public void SingleDiscountIsApplied_WhenThereAreExactNumberOfProducts(string discountName, string productName, int itemsRequiredToApplyDiscount, decimal specialGroupPrice, decimal regularPrice)
		{
			var bulkDiscount = new BulkDiscount(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<Product>();

			int numberOfProducts = itemsRequiredToApplyDiscount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(new Product(productName, regularPrice));
			}

			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			var expectedResultPrice = specialGroupPrice - numberOfProducts * regularPrice;
			discountPrice.Should().Be(expectedResultPrice);
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40, 2)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20, 3)]
		[TestCase("Sale discount", "Product3", 2, 80, 50, 325)]
		public void MultipleDiscountsAreApplied_WhenThereAreProductsForMultipleBulkDiscounts(string discountName,
			string productName,
			int itemsRequiredToApplyDiscount,
			decimal specialGroupPrice,
			decimal regularPrice,
			int bulkItemsSets)
		{
			var bulkDiscount = new BulkDiscount(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<Product>();

			int numberOfProducts = bulkItemsSets*itemsRequiredToApplyDiscount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(new Product(productName, regularPrice));
			}

			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			var expectedResultPrice = bulkItemsSets*specialGroupPrice - numberOfProducts * regularPrice;
			discountPrice.Should().Be(expectedResultPrice);
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20)]
		[TestCase("Sale discount", "Product3", 2, 80, 50)]
		public void DiscountIsNotSet_ForProductsAboveTheBulkDiscount(string discountName, 
			string productName,
			int itemsRequiredToApplyDiscount, 
			decimal specialGroupPrice, 
			decimal regularPrice)
		{
			var bulkDiscount = new BulkDiscount(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<Product>();

			int additionalProductsAmount = itemsRequiredToApplyDiscount-1;
			int numberOfProducts = itemsRequiredToApplyDiscount + additionalProductsAmount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(new Product(productName, regularPrice));
			}

			var discountPrice = bulkDiscount.Calculate(products.ToArray());
			var priceWithoutDiscounts = numberOfProducts * regularPrice;

			discountPrice.Should().Be(specialGroupPrice- priceWithoutDiscounts);
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20)]
		[TestCase("Sale discount", "Product3", 2, 80, 50)]
		public void DiscountIsNotSet_WhenAmountIsBelowTheBulkDiscount(string discountName, 
			string productName,
			int itemsRequiredToApplyDiscount, 
			decimal specialGroupPrice, 
			decimal regularPrice)
		{
			var bulkDiscount = new BulkDiscount(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<Product>();
			int amountOfProducts = itemsRequiredToApplyDiscount - 1;

			for (int i = 0; i < amountOfProducts; i++)
			{
				products.Add(new Product(productName, regularPrice));
			}

			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			discountPrice.Should().Be(0);
		}
	}
}