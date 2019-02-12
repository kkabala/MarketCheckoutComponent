using FluentAssertions;
using Market.CheckoutComponent.Model.DiscountRules;
using Market.CheckoutComponent.Model.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.CheckoutComponent.Tests.Model.Discounts
{
	[TestFixture]
	public class PriceThresholdDiscountRuleTests
	{
		[TestCase("Christmas package discount", 600, 10)]
		[TestCase("Christmas package discount", 1300, 25)]
		[TestCase("Christmas package discount", 100, 5)]
		public void Calculate_ReturnsDiscount_WhenTotalProductsPriceIsEqualToThreshold(string discountName, int priceThreshold, int discountPercentage)
		{
			//Arrange
			var packageDiscount = new PriceThresholdDiscountRule(discountName, priceThreshold, discountPercentage);
			var productsInTheBasket = GetProducts(priceThreshold);

			//Act
			var appliedDiscount = packageDiscount.Calculate(productsInTheBasket);

			//Assert
			appliedDiscount.Should().Be(-(discountPercentage * priceThreshold) / 100);
		}

		[TestCase("Christmas package discount", 600, 10)]
		[TestCase("Christmas package discount", 1300, 25)]
		[TestCase("Christmas package discount", 100, 5)]
		public void Calculate_ReturnsSingleDiscount_WhenDiscountConditionsAreMetMultipleTimes(string discountName, int priceThreshold, int discountPercentage)
		{
			//Arrange
			var priceThresholdDiscount = new PriceThresholdDiscountRule(discountName, priceThreshold, discountPercentage);
			var productsInTheBasket = GetProducts(5 * priceThreshold).ToList();

			//Act
			var appliedDiscount = priceThresholdDiscount.Calculate(productsInTheBasket);
			var productsSum = productsInTheBasket.Sum(m => m.Price);

			//Assert
			appliedDiscount.Should().Be(-(discountPercentage * productsSum) / 100);
		}

		[TestCase("Christmas package discount", 600, 10)]
		[TestCase("Christmas package discount", 1300, 25)]
		[TestCase("Christmas package discount", 100, 5)]
		public void Calculate_ReturnsZeroDiscount_WhenNullProductsListIsPassed(string discountName, int priceThreshold, int discountPercentage)
		{
			//Arrange
			var priceThresholdDiscount = new PriceThresholdDiscountRule(discountName, priceThreshold, discountPercentage);

			//Act
			var appliedDiscount = priceThresholdDiscount.Calculate(null);

			//Assert
			appliedDiscount.Should().Be(0);
		}

		[TestCase("Christmas package discount", 600, 10)]
		[TestCase("Christmas package discount", 1300, 25)]
		[TestCase("Christmas package discount", 100, 5)]
		public void Calculate_ReturnsZeroDiscount_WhenTotalPriceIsBelowThreshold(string discountName, int priceThreshold, int discountPercentage)
		{
			//Arrange
			var packageDiscount = new PriceThresholdDiscountRule(discountName, priceThreshold, discountPercentage);
			var productsInTheBasket = GetProducts(priceThreshold - 1);

			//Act
			var appliedDiscount = packageDiscount.Calculate(productsInTheBasket);

			//Assert
			appliedDiscount.Should().Be(0);
		}

		private IEnumerable<IProduct> GetProducts(int totalPrice)
		{
			var products = new List<IProduct>();
			var moneyLeft = totalPrice;

			var priceGenerator = new Random();
			while (moneyLeft > 0)
			{
				var productMock = new Mock<IProduct>();
				var price = priceGenerator.Next(1, moneyLeft);
				productMock.SetupGet(m => m.Price).Returns(price);
				products.Add(productMock.Object);
				moneyLeft -= price;
			}

			return products;
		}
	}
}