using FluentAssertions;
using Market.CheckoutComponent;
using Market.WebApi.Utilities;
using NUnit.Framework;

namespace Market.WebApi.Tests.Utilities
{
	[TestFixture]
	public class ProductsBasketFactoryTests
	{
		[Test]
		public void Create_ReturnsNewInstanceOfBasket()
		{
			//Arrange
			var factory = new ProductsBasketFactory(null, null);

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
