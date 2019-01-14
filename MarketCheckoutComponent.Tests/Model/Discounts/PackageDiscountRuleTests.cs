using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MarketCheckoutComponent.Model.DiscountRules;
using MarketCheckoutComponent.Model.Interfaces;
using Moq;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests.Model.Discounts
{
	[TestFixture]
	public class PackageDiscountRuleTests
	{
		[TestCase("Christmas package discount", 10, new []{"ProductA", "ProductB"})]
		[TestCase("Holiday package discount", 30, new []{"ProductA", "ProductB", "ProductC", "ProductD"})]
		[TestCase("Sale package discount", 80, new []{"ProductA", "ProductB", "ProductC", "ProductD", "ProductE"})]
		public void Calculate_ReturnsCorrectDiscount_WhenDiscountConditionsAreMet(string discountName, decimal discountAmount, string[] packageProductNames)
		{
			//Arrange
			var packageDiscount = new PackageDiscountRule(discountName, discountAmount, packageProductNames);
			var productsInTheBasket = GetProducts(packageProductNames);

			//Act
			var appliedDiscount = packageDiscount.Calculate(productsInTheBasket);

			//Assert
			appliedDiscount.Should().Be(discountAmount);
		}

		[TestCase("Christmas package discount", 10, new []{"ProductA", "ProductB"})]
		[TestCase("Holiday package discount", 30, new []{"ProductA", "ProductB", "ProductC", "ProductD"})]
		[TestCase("Sale package discount", 80, new []{"ProductA", "ProductB", "ProductC", "ProductD", "ProductE"})]
		public void Calculate_ReturnsZeroDiscount_WhenDiscountConditionsAreNotMet(string discountName, decimal discountAmount, string[] packageProductNames)
		{
			//Arrange
			var packageDiscount = new PackageDiscountRule(discountName, discountAmount, packageProductNames);
			var productsInTheBasket = GetProducts(packageProductNames.Take(packageProductNames.Length-1));

			//Act
			var appliedDiscount = packageDiscount.Calculate(productsInTheBasket);

			//Assert
			appliedDiscount.Should().Be(0);
		}

		private IEnumerable<IProduct> GetProducts(IEnumerable<string> productNames)
		{
			var products = new List<IProduct>();

			var priceGenerator = new Random();
			foreach (var singleProductName in productNames)
			{
				var productMock = new Mock<IProduct>();
				productMock.SetupGet(m => m.Name).Returns(singleProductName);
				productMock.SetupGet(m => m.Price).Returns(priceGenerator.Next(50, 1000));

				products.Add(productMock.Object);
			}

			return products;
		}

		[TestCase("Christmas package discount", 10, new[] { "ProductA", "ProductB" })]
		[TestCase("Holiday package discount", 30, new[] { "ProductA", "ProductB", "ProductC", "ProductD" })]
		[TestCase("Sale package discount", 80, new[] { "ProductA", "ProductB", "ProductC", "ProductD", "ProductE" })]
		public void Calculate_ReturnsMultipleDiscounts_WhenDiscountConditionsAreMetMultipleTimes(string discountName, decimal discountAmount, string[] packageProductNames)
		{
			//Arrange
			var packageDiscount = new PackageDiscountRule(discountName, discountAmount, packageProductNames);
			var productsInTheBasket = GetProducts(packageProductNames.Concat(packageProductNames));

			//Act
			var appliedDiscount = packageDiscount.Calculate(productsInTheBasket);

			//Assert
			appliedDiscount.Should().Be(2*discountAmount);
		}

		[TestCase("Christmas package discount", 10, new[] { "ProductA", "ProductB" })]
		[TestCase("Holiday package discount", 30, new[] { "ProductA", "ProductB", "ProductC", "ProductD" })]
		[TestCase("Sale package discount", 80, new[] { "ProductA", "ProductB", "ProductC", "ProductD", "ProductE" })]
		public void Calculate_ReturnsZeroDiscount_WhenNullProductsListIsPassed(string discountName, decimal discountAmount, string[] packageProductNames)
		{
			//Arrange
			var packageDiscount = new PackageDiscountRule(discountName, discountAmount, packageProductNames);

			//Act
			var appliedDiscount = packageDiscount.Calculate(null);

			//Assert
			appliedDiscount.Should().Be(0);
		}
	}
}