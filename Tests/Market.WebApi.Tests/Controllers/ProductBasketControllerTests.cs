using FluentAssertions;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Controllers;
using Market.WebApi.Services;
using Moq;
using NUnit.Framework;

namespace Market.WebApi.Tests.Controllers
{
	[TestFixture]
	public class ProductBasketControllerTests
	{
		[Test]
		public void Checkout_ReturnsBill()
		{
			//Arrange
			var controller = new ProductBasketController(null, GetSalesHistoryServiceMock().Object);

			//Act
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Should().BeOfType<Bill>();
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMock()
		{
			return new Mock<ISalesHistoryService>();
		}

		[Test]
		public void Checkout_ReturnsBillWithPreviouslyAddedProducts()
		{
			//Arrange
			var product = GetMockedProduct("A", 50);
			var dataService = new Mock<IDataService>();
			dataService.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns(product);
			var controller = new ProductBasketController(dataService.Object, GetSalesHistoryServiceMock().Object);

			//Act
			controller.Add(product.Name);
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Products.Should().Contain(product);
		}

		private IProduct GetMockedProduct(string name, decimal price)
		{
			var mock = new Mock<IProduct>();
			mock.Setup(m => m.Name).Returns(name);
			mock.Setup(m => m.Price).Returns(price);
			return mock.Object;
		}
	}
}
