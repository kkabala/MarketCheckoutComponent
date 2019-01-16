using FluentAssertions;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Controllers;
using Market.WebApi.Services;
using Market.WebApi.Utilities.Interfaces;
using Moq;
using NUnit.Framework;

namespace Market.WebApi.Tests.Controllers
{
	[TestFixture]
	public class ProductBasketControllerTests
	{
		[Test]
		public void Checkout_ReturnsBillInTextForm()
		{
			//Arrange
			var product = GetMockedProduct("A", 50);
			var dataService = new Mock<IDataService>();
			dataService.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns(product);
			var controller = new ProductBasketController(dataService.Object, GetProductsBasketFactoryMock().Object);

			//Act
			controller.Add(product.Name);
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Should().Be(BillsTestToStringMethodOutput);
		}

		private IProduct GetMockedProduct(string name, decimal price)
		{
			var mock = new Mock<IProduct>();
			mock.Setup(m => m.Name).Returns(name);
			mock.Setup(m => m.Price).Returns(price);
			return mock.Object;
		}

		private const string BillsTestToStringMethodOutput = "Test ToString method output";

		private Mock<IProductsBasketFactory> GetProductsBasketFactoryMock()
		{
			var mock = new Mock<IProductsBasketFactory>();
			var basketMock = new Mock<IProductsBasket>();
			var billMock = new Mock<IBill>();
			billMock.Setup(m => m.ToString()).Returns(BillsTestToStringMethodOutput);
			basketMock.Setup(m => m.Checkout()).Returns(billMock.Object);
			mock.Setup(m => m.Create()).Returns(basketMock.Object);

			return mock;
		}
	}
}
