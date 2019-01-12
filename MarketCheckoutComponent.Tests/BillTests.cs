using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Model.Discounts.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace MarketCheckoutComponent.Tests
{
	[TestFixture]
	public class BillTests
	{
		private Bill bill;
		private string[] productNames = new string[] { "Test1", "Test2", "Test3" };

		private readonly decimal[] productPrices = new decimal[] { 5m, 2.5m, 6m };
		private Product[] products;
		private IDiscount[] discounts;

		[TearDown]
		public void TearDown()
		{
			products = null;
			discounts = null;
		}

		public void SetUpAllProducts()
		{
			products = new[]
			{
				new Product(productNames[0], productPrices[0]),
				new Product(productNames[0], productPrices[0]),
				new Product(productNames[0], productPrices[0]),

				new Product(productNames[1], productPrices[1]),

				new Product(productNames[2], productPrices[2]),
				new Product(productNames[2], productPrices[2]),
			};
		}

		public void SetUpAllDiscounts()
		{
			var discountMock1 = new Mock<IDiscount>();

			discountMock1.Setup(m => m.Name).Returns("Christmas discount");
			discountMock1.Setup(m => m.Calculate(It.IsAny<Product[]>())).Returns(-50m);

			var discountMock2 = new Mock<IDiscount>();
			discountMock2.Setup(m => m.Name).Returns("Sale discount");
			discountMock2.Setup(m => m.Calculate(It.IsAny<Product[]>())).Returns(-100m);

			discounts = new[]
			{
				discountMock1.Object,
				discountMock2.Object
			};
		}

		[Test]
		public void ToString_ReturnsEntriesForEachProductType()
		{
			SetUpAllProducts();
			bill = new Bill(products, discounts);

			var result = bill.ToString();

			for (int i = 0; i < productNames.Length; i++)
			{
				result.Should().ContainAll(productNames[i], productPrices[i].ToString("F2"));
			}
		}

		[Test]
		public void ToString_ReturnsGroupedEntries()
		{
			SetUpAllProducts();
			bill = new Bill(products, discounts);
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
			bill = new Bill(null, discounts);
			var result = bill.ToString().Split("\n");

			foreach (var singleDiscount in discounts)
			{
				var discountInfo = result.Single(m => m.Contains(singleDiscount.Name));
				discountInfo.Should().Contain(singleDiscount.Calculate(null).ToString("F2"));
			}
		}
	}
}
