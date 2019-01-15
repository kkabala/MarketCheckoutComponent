﻿using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MarketCheckoutComponent.Tests.Model
{
	[TestFixture]
	public class BillTests
	{
		private readonly string[] productNames = { "Test1", "Test2", "Test3" };

		private readonly decimal[] productPrices = { 55m, 29.512m, 68m };
		private readonly int discount1Amount = -51;
		private readonly int discount2Amount = -33;

		private IProduct[] products;
		private IDiscountRule[] discountsRules;

		private readonly int productTestDataCopies = 3;

		[TearDown]
		public void TearDown()
		{
			products = null;
			discountsRules = null;
		}

		public void SetUpAllProducts()
		{
			var productsList = new List<IProduct>();

			for (int i = 0; i < productTestDataCopies; i++)
			{
				productsList.Add(new Product(productNames[0], productPrices[0]));
				productsList.Add(new Product(productNames[1], productPrices[1]));
				productsList.Add(new Product(productNames[2], productPrices[2]));
			};

			products = productsList.ToArray();
		}

		public void SetUpAllDiscounts()
		{
			var discountMock1 = new Mock<IDiscountRule>();

			discountMock1.Setup(m => m.Name).Returns("Christmas discount");
			discountMock1.Setup(m => m.Calculate(It.IsAny<IProduct[]>())).Returns(discount1Amount);

			var discountMock2 = new Mock<IDiscountRule>();
			discountMock2.Setup(m => m.Name).Returns("Sale discount");
			discountMock2.Setup(m => m.Calculate(It.IsAny<IProduct[]>())).Returns(discount2Amount);

			discountsRules = new[]
			{
				discountMock1.Object,
				discountMock2.Object
			};
		}

		[Test]
		public void ToString_ReturnsHeaderWithBillColumnNames()
		{
			var billColumnNames = new[] { "Product", "Price", "Unit", "Amount" };
			SetUpAllProducts();
			var bill = new Bill(products, discountsRules);

			//Act
			var headerLine = bill.ToString().Split("\n").First();

			//Assert
			headerLine.Should().ContainAll(billColumnNames);
		}


		[Test]
		public void ToString_ReturnsEntriesForEachProductType()
		{
			//Arrange
			SetUpAllProducts();
			var bill = new Bill(products, discountsRules);

			//Act
			var result = bill.ToString();

			//Assert
			for (int i = 0; i < productNames.Length; i++)
			{
				result.Should().ContainAll(productNames[i], productPrices[i].ToString("F2"));
			}
		}

		[Test]
		public void ToString_ReturnsGroupedProductsWithAllColumnsFilled()
		{
			//Arrange
			SetUpAllProducts();
			var bill = new Bill(products, discountsRules);
			const int linesForHeaderAndFooter = 2;

			//Act
			var result = bill.ToString().Split("\n");

			//Assert
			//skip 0,1 as 0 & 1 are the bill's header
			//skil last, last-1 as they're the bill's footer
			for (int i = linesForHeaderAndFooter; i < result.Length - linesForHeaderAndFooter; i++)
			{
				var singleBillLine = result[i];
				var currentProduct = products.First(m => singleBillLine.Contains(m.Name));
				var numberOfProducts = products.Count(m => m.Name == currentProduct.Name);
				var productPrice = currentProduct.Price;
				var amount = (productPrice * numberOfProducts).ToString("F2");

				singleBillLine.Should().ContainAll(currentProduct.Name, 
					productPrice.ToString("F2"), 
					numberOfProducts.ToString(), 
					amount);
			}
		}

		[Test]
		public void ToString_ReturnsInfoAboutAllDiscounts()
		{
			//Arrange
			SetUpAllDiscounts();
			var bill = new Bill(null, discountsRules);

			//Act
			var result = bill.ToString().Split("\n");

			//Assert
			foreach (var singleDiscount in discountsRules)
			{
				var discountInfo = result.Single(m => m.Contains(singleDiscount.Name));
				discountInfo.Should().Contain(singleDiscount.Calculate(null).ToString("F2"));
			}
		}

		[Test]
		public void ToString_ReturnsInfoAboutTotalPrice()
		{
			//Arrange
			SetUpAllProducts();
			var bill = new Bill(products, null);
			string totalFooter = "Total: ";


			//Act
			var result = bill.ToString().Split("\n");
			var lastLine = result.Last();

			//Assert
			lastLine.Should().Contain(totalFooter);
			lastLine.Should().Contain(bill.Total.ToString());
			lastLine.Length.Should().Be(totalFooter.Length + bill.Total.ToString().Length);
		}

		[Test]
		public void Total_ReturnsSumOfProductPrices()
		{
			//Arrange
			SetUpAllProducts();
			var bill = new Bill(products, null);

			//Act
			var total = bill.Total;

			//Assert
			total.Should().Be(productTestDataCopies * productPrices.Sum());
		}

		[Test]
		public void Total_SubstractsValueForAppliedDiscounts()
		{
			//Arrange
			SetUpAllProducts();
			SetUpAllDiscounts();
			var bill = new Bill(products, discountsRules);
			var productsPricesSum = productPrices.Sum() * productTestDataCopies;
			var discountsSum = discount1Amount + discount2Amount;

			//Act
			var total = bill.Total;

			//Assert
			total.Should().Be(productsPricesSum + discountsSum);
		}
	}
}