using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services;
using MarketCheckoutComponent.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests
{
	public class ProductsBasketTests
	{
		[TestCase(0)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(2)]
		[TestCase(1)]
		public void AddProduct_AddsNewEntryToTheBasket(int numberOfProducts)
		{
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);
			for (int i = 0; i < numberOfProducts; i++)
			{
				productsBasket.AddProduct(new Product());
			}

			var bill = productsBasket.Checkout();

			bill.Products.Should().NotBeNull();
			bill.Products.Length.Should().Be(numberOfProducts);
		}

		[Test]
		public void Checkout_ReturnsBill()
		{
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);
			var bill = productsBasket.Checkout();

			bill.Should().NotBeNull();
		}

		[Test]
		public void Checkout_SavesBillToSalesHistory()
		{
			var salesHistoryServiceMock = GetSalesHistoryServiceMockWithActions();
			var productsBasket = new ProductsBasket(salesHistoryServiceMock.Object);
			var bill = productsBasket.Checkout();

			salesHistoryServiceMock.Verify(m => m.Add(It.IsAny<Bill>()), Times.Once);
			bill.Should().NotBeNull();
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMockWithNoSetup()
		{
			return new Mock<ISalesHistoryService>();
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMockWithActions()
		{
			var mock = new Mock<ISalesHistoryService>();
			mock.Setup(m => m.Add(It.IsAny<Bill>()));
			return mock;
		}
	}
}