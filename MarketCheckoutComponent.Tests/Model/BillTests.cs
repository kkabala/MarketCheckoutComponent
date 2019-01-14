using System.Linq;
using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.DiscountRules.Interfaces;
using MarketCheckoutComponent.Model.Interfaces;
using Moq;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests.Model
{
	[TestFixture]
	public class BillTests
	{
		private readonly string[] productNames = { "Test1", "Test2", "Test3" };

		private readonly decimal[] productPrices = { 5m, 2.5m, 6m };
		private IProduct[] products;
		private IDiscountRule[] discountsRule;

		[TearDown]
		public void TearDown()
		{
			products = null;
			discountsRule = null;
		}

		public void SetUpAllProducts()
		{
			products = new IProduct[]
			{
				new Product(productNames[0], productPrices[0]),
				new Product(productNames[0], productPrices[0]),
				new Product(productNames[0], productPrices[0]),

				new Product(productNames[1], productPrices[1]),

				new Product(productNames[2], productPrices[2]),
				new Product(productNames[2], productPrices[2])
			};
		}

		public void SetUpAllDiscounts()
		{
			var discountMock1 = new Mock<IDiscountRule>();

			discountMock1.Setup(m => m.Name).Returns("Christmas discount");
			discountMock1.Setup(m => m.Calculate(It.IsAny<IProduct[]>())).Returns(-50m);

			var discountMock2 = new Mock<IDiscountRule>();
			discountMock2.Setup(m => m.Name).Returns("Sale discount");
			discountMock2.Setup(m => m.Calculate(It.IsAny<IProduct[]>())).Returns(-100m);

			discountsRule = new[]
			{
				discountMock1.Object,
				discountMock2.Object
			};
		}

		[Test]
		public void ToString_ReturnsEntriesForEachProductType()
		{
			SetUpAllProducts();
			var bill = new Bill(products, discountsRule);

			var result = bill.ToString();

			for (int i = 0; i < productNames.Length; i++)
			{
				result.Should().ContainAll(productNames[i], productPrices[i].ToString("F2"));
			}
		}

		[Test]
		public void ToString_ReturnsGroupedProducts()
		{
			SetUpAllProducts();
			var bill = new Bill(products, discountsRule);
			var result = bill.ToString().Split("\n");

			//skip 0,1 as 0 & 1 are the bill header
			for (int i = 2; i < result.Length; i++)
			{
				var singleBillLine = result[i];
				var currentProduct = products.First(m => singleBillLine.Contains(m.Name));
				var numberOfProducts = products.Count(m => m.Name == currentProduct.Name);
				singleBillLine.Should().Contain(numberOfProducts.ToString());
			}
		}

		[Test]
		public void ToString_ReturnsInfoAboutAllDiscounts()
		{
			SetUpAllProducts();
			SetUpAllDiscounts();
			var bill = new Bill(null, discountsRule);
			var result = bill.ToString().Split("\n");

			foreach (var singleDiscount in discountsRule)
			{
				var discountInfo = result.Single(m => m.Contains(singleDiscount.Name));
				discountInfo.Should().Contain(singleDiscount.Calculate(null).ToString("F2"));
			}
		}
	}
}