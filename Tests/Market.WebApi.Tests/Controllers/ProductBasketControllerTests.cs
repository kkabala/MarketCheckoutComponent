using FluentAssertions;
using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Interfaces;
using Market.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace Market.WebApi.Tests.Controllers
{
	[TestFixture]
	public class ProductBasketControllerTests
	{
		private const string billText = "Test ToString method output";

		[Test]
		public void Checkout_ReturnsBillInTextForm()
		{
			//Arrange
			var mock = new Mock<IProductBasketService>();
			mock.Setup(m => m.Checkout()).Returns(billText);

			var controller = new BasketController(mock.Object);

			//Act
			controller.AddProduct("test");
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Should().Be(billText);
		}

		[Test]
		public void DecreaseUnits_ExecutesUnderlyingMethodInTheBasket()
		{
			//Arrange
			var basketService = new Mock<IProductBasketService>();
			basketService.Setup(m => m.DecreaseUnits(It.IsAny<string>()));

			//Act
			var controller1 = new BasketController(basketService.Object);
			controller1.DecreaseUnits("s");

			//Assert
			basketService.Verify(m => m.DecreaseUnits(It.IsAny<string>()), Times.Once);
		}
	}
}