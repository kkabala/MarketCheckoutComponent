using FluentAssertions;
using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Utilities.Interfaces;
using Moq;
using NUnit.Framework;

namespace Market.Services.Tests.Services
{
	[TestFixture]
	public class ProductBasketServiceTests
	{
		[Test]
		public void GetCurrent_ReturnsTheSameInstanceEveryTime()
		{
			//Arrange
			var productsBasketFactoryMock = GetDefaultFactory();
			var service = new ProductBasketService(productsBasketFactoryMock);

			//Act
			var instance1 = service.GetCurrent();
			var instance2 = service.GetCurrent();
			var instance3 = service.GetCurrent();

			//Assert
			instance1.GetHashCode().Should().Be(instance2.GetHashCode());
			instance2.GetHashCode().Should().Be(instance3.GetHashCode());
			instance3.GetHashCode().Should().Be(instance1.GetHashCode());
		}

		private IProductsBasketFactory GetDefaultFactory()
		{
			var productsBasketFactoryMock = new Mock<IProductsBasketFactory>();
			productsBasketFactoryMock.Setup(m => m.Create()).Returns(() =>
			{
				var salesHistory = new Mock<ISalesHistoryService>().Object;
				var dataServiceMock = new Mock<IProductDataService>().Object;
				return new ProductsBasket(salesHistory, null, dataServiceMock);
			});

			return productsBasketFactoryMock.Object;
		}

		[Test]
		public void Reset_ForcesCreatingNewBasketInsance_WhenGetCurrentIsCalled()
		{
			//Arrange
			var productsBasketFactoryMock = GetDefaultFactory();
			var service = new ProductBasketService(productsBasketFactoryMock);

			//Act
			var instance1 = service.GetCurrent();
			service.Reset();
			var instance2 = service.GetCurrent();
			service.Reset();
			var instance3 = service.GetCurrent();

			//Assert
			instance1.GetHashCode().Should().NotBe(instance2.GetHashCode());
			instance2.GetHashCode().Should().NotBe(instance3.GetHashCode());
			instance3.GetHashCode().Should().NotBe(instance1.GetHashCode());
		}
	}
}
