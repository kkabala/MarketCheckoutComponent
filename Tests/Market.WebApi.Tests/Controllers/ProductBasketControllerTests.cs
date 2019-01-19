using FluentAssertions;
using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
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
			var controller = new BasketController(dataService.Object, GetBasketProvider().Object);

			//Act
			controller.AddProduct(product.Name);
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Should().Be(BillsTestToStringMethodOutput);
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

			var factoryMock = new Mock<IProductBasketProviderService>();
			factoryMock.Setup(m => m.GetCurrent()).Returns(basketMock.Object);

			//Act
			var controller1 = new BasketController(dataService.Object, factoryMock.Object);
			controller1.DecreaseUnits(product.Name);

			//Assert
			basketMock.Verify(m => m.DecreaseUnits(It.IsAny<string>()), Times.Once);
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(5)]
		public void Checkout_ResetsTheBasket(int numberOfTimesThatCheckoutIsExecuted)
		{
			//Arrange
			var product = GetMockedProduct("A", 50);
			var dataService = new Mock<IDataService>();
			dataService.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns(product);
			var saleshistoryServiceMock = new Mock<ISalesHistoryService>();
			var basketProviderService = new Mock<IProductBasketProviderService>();
			basketProviderService.Setup(m => m.Reset());
			basketProviderService.Setup(m => m.GetCurrent()).Returns(new ProductsBasket(saleshistoryServiceMock.Object,null));
			var controller = new BasketController(dataService.Object, 
				basketProviderService.Object);

			//Act
			for (int i = 0; i < numberOfTimesThatCheckoutIsExecuted; i++)
			{
				controller.AddProduct(product.Name);
				controller.Checkout();
			}

			//Assert
			basketProviderService.Verify(m=> m.Reset(),Times.Exactly(numberOfTimesThatCheckoutIsExecuted));
		}

		private IProduct GetMockedProduct(string name, decimal price)
		{
			var mock = new Mock<IProduct>();
			mock.Setup(m => m.Name).Returns(name);
			mock.Setup(m => m.Price).Returns(price);
			return mock.Object;
		}

		private const string BillsTestToStringMethodOutput = "Test ToString method output";

		private Mock<IProductBasketProviderService> GetBasketProvider()
		{
			var billMock = new Mock<IBill>();
			billMock.Setup(m => m.ToString()).Returns(BillsTestToStringMethodOutput);

			var basketMock = new Mock<IProductsBasket>();
			basketMock.Setup(m => m.Checkout()).Returns(billMock.Object);

			var mock = new Mock<IProductBasketProviderService>();
			mock.Setup(m => m.GetCurrent()).Returns(basketMock.Object);

			return mock;
		}
	}
}