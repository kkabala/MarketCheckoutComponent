using System.Threading.Tasks;
using FluentAssertions;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using Market.WebApi.Controllers;
using Market.WebApi.Services.Interfaces;
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
			var controller = new BasketController(dataService.Object, GetProductsBasketFactoryMock().Object);

			//Act
			controller.AddProduct(product.Name);
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Should().Be(BillsTestToStringMethodOutput);
		}

		[Test]
		public void BasketIsSavedBetweenControllerInstances()
		{
			//Arrange
			var product = GetMockedProduct("A", 50);
			var dataService = new Mock<IDataService>();
			dataService.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns(product);
			var factory = GetProductsBasketFactoryMock();

			//Act
			var controller1 = new BasketController(dataService.Object, factory.Object);
			var controller2 = new BasketController(dataService.Object, factory.Object);

			//Assert
			factory.Verify(m => m.Create(), Times.Once);
		}

		[Test]
		public void DecreaseUnits_ExecutesUnderlyingMethodInTheBasket()
		{
			//Arrange
			var product = GetMockedProduct("A", 50);
			var dataService = new Mock<IDataService>();
			dataService.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns(product);

			var billMock = new Mock<IBill>();
			billMock.Setup(m => m.ToString()).Returns(BillsTestToStringMethodOutput);

			var basketMock = new Mock<IProductsBasket>();
			basketMock.Setup(m => m.Checkout()).Returns(billMock.Object);

			var factoryMock = new Mock<IProductsBasketFactory>();
			factoryMock.Setup(m => m.Create()).Returns(basketMock.Object);

			//Act
			var controller1 = new BasketController(dataService.Object, factoryMock.Object);
			controller1.DecreaseUnits(product.Name);

			//Assert
			basketMock.Verify(m => m.DecreaseUnits(It.IsAny<string>()), Times.Once);
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
			var billMock = new Mock<IBill>();
			billMock.Setup(m => m.ToString()).Returns(BillsTestToStringMethodOutput);

			var basketMock = new Mock<IProductsBasket>();
			basketMock.Setup(m => m.Checkout()).Returns(billMock.Object);

			var mock = new Mock<IProductsBasketFactory>();
			mock.Setup(m => m.Create()).Returns(basketMock.Object);

			return mock;
		}
	}
}