using System.Collections.Generic;
using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.DiscountRules;
using MarketCheckoutComponent.Model.Interfaces;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests.Model.Discounts
{
	[TestFixture]
	public class BulkDiscountRuleTests
	{
		private ProductsMockObjectsGenerator productsGenerator;

		[SetUp]
		public void SetUp()
		{
			this.productsGenerator = new ProductsMockObjectsGenerator();
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20)]
		[TestCase("Sale discount", "Product3", 2, 80, 50)]
		public void Calculate_AppliesSingleDiscount_WhenThereAreExactNumberOfProducts(string discountName, string productName, int itemsRequiredToApplyDiscount, decimal specialGroupPrice, decimal regularPrice)
		{
			//Arrange
			var bulkDiscount = new BulkDiscountRuleRule(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<IProduct>();

			int numberOfProducts = itemsRequiredToApplyDiscount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(productsGenerator.Generate(productName, regularPrice));
			}

			//Act
			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			//Assert
			var expectedResultPrice = specialGroupPrice - numberOfProducts * regularPrice;
			discountPrice.Should().Be(expectedResultPrice);
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40, 2)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20, 3)]
		[TestCase("Sale discount", "Product3", 2, 80, 50, 325)]
		public void Calculate_AppliesMultipleDiscounts_WhenThereAreProductsForMultipleBulkDiscounts(string discountName,
			string productName,
			int itemsRequiredToApplyDiscount,
			decimal specialGroupPrice,
			decimal regularPrice,
			int bulkItemsSets)
		{
			//Arrange
			var bulkDiscount = new BulkDiscountRuleRule(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<IProduct>();

			int numberOfProducts = bulkItemsSets*itemsRequiredToApplyDiscount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(productsGenerator.Generate(productName, regularPrice));
			}

			//Act
			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			//Assert
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
			//Arrange
			var bulkDiscount = new BulkDiscountRuleRule(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<IProduct>();

			int additionalProductsAmount = itemsRequiredToApplyDiscount-1;
			int numberOfProducts = itemsRequiredToApplyDiscount + additionalProductsAmount;
			for (int i = 0; i < numberOfProducts; i++)
			{
				products.Add(productsGenerator.Generate(productName, regularPrice));
			}

			//Act
			var discountPrice = bulkDiscount.Calculate(products.ToArray());
			var priceWithoutDiscounts = numberOfProducts * regularPrice;

			//Assert
			discountPrice.Should().Be(specialGroupPrice- priceWithoutDiscounts);
		}

		[TestCase("Christmas discount", "Product1", 3, 70, 40)]
		[TestCase("Birthday discount", "Product2", 30, 520, 20)]
		[TestCase("Sale discount", "Product3", 2, 80, 50)]
		public void CalculateReturnsZeroDiscount_WhenAmountIsBelowTheBulkDiscount(string discountName, 
			string productName,
			int itemsRequiredToApplyDiscount, 
			decimal specialGroupPrice, 
			decimal regularPrice)
		{
			//Arrange
			var bulkDiscount = new BulkDiscountRuleRule(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			//Act
			var discountPrice = bulkDiscount.Calculate(null);

			//Assert
			discountPrice.Should().Be(0);
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
			//Arrange
			var bulkDiscount = new BulkDiscountRuleRule(discountName,
				productName,
				itemsRequiredToApplyDiscount,
				specialGroupPrice);

			var products = new List<IProduct>();
			int amountOfProducts = itemsRequiredToApplyDiscount - 1;

			for (int i = 0; i < amountOfProducts; i++)
			{
				products.Add(productsGenerator.Generate(productName, regularPrice));
			}

			//Act
			var discountPrice = bulkDiscount.Calculate(products.ToArray());

			//Assert
			discountPrice.Should().Be(0);
		}
	}
}