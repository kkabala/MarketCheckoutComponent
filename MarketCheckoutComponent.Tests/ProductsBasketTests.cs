using FluentAssertions;
using MarketCheckoutComponent.Model;
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
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);
			for (int i = 0; i < numberOfProducts; i++)
			{
				productsBasket.AddProduct(new Product());
			}

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.Products.Should().NotBeNull();
			bill.Products.Length.Should().Be(numberOfProducts);
		}

		[Test]
		public void Checkout_ReturnsBill()
		{
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.Should().NotBeNull();
		}

		[Test]
		public void Checkout_SavesBillToSalesHistory()
		{
			//Arrange
			var salesHistoryServiceMock = GetSalesHistoryServiceMockWithActions();
			var productsBasket = new ProductsBasket(salesHistoryServiceMock.Object);

			//Act
			var bill = productsBasket.Checkout();

			//Assert
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