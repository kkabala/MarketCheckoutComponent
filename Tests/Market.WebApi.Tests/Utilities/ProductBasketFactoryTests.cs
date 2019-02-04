using FluentAssertions;
using Market.CheckoutComponent;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Utilities;
using Moq;
using NUnit.Framework;

namespace Market.WebApi.Tests.Utilities
{
	[TestFixture]
	public class ProductBasketFactoryTests
	{
		[Test]
		public void Create_ReturnsNewInstanceOfBasket()
		{
			//Arrange
			var salesHistoryMock = new Mock<ISalesHistoryService>();
			var factory = new ProductsBasketFactory(salesHistoryMock.Object, null);

			//Act
			var basket1 = factory.Create();
			var basket2 = factory.Create();

			//Assert
			basket1.Should().BeOfType<ProductsBasket>();
			basket2.Should().BeOfType<ProductsBasket>();
			basket1.Should().NotBe(basket2);
		}
	}
}