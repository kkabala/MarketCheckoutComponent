using FluentAssertions;
using Market.CheckoutComponent;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services.Utilities.Interfaces;
using Moq;
using NUnit.Framework;

namespace Market.Services.Tests.Services
{
	[TestFixture]
	public class ProductBasketServiceTests
	{
		Mock<IProductsBasket> currentProductBasketMock;
		Mock<IProductsBasketFactory> productsBasketFactoryMock;

		[SetUp]
		public void SetUp()
		{
			currentProductBasketMock = new Mock<IProductsBasket>();
			currentProductBasketMock.Setup(m => m.Checkout()).Returns(() =>
			  {
				  var billMock = new Mock<IBill>();
				  return billMock.Object;
			  });
		}

		[TestCase(0)]
		[TestCase(5)]
		[TestCase(100)]
		[TestCase(4120)]
		public void Checkout_ForcesCreatingNewBasketInsance(int numberOfCheckouts)
		{
			//Arrange
			var productsBasketFactory = GetDefaultFactory();
			var service = new ProductBasketService(productsBasketFactory);

			//Act
			for (int i =0; i< numberOfCheckouts; i++)
				service.Checkout();


			//Assert
			productsBasketFactoryMock.Verify(m => m.Create(), Times.Exactly(numberOfCheckouts));
		}

		private IProductsBasketFactory GetDefaultFactory()
		{
			productsBasketFactoryMock = new Mock<IProductsBasketFactory>();
			productsBasketFactoryMock.Setup(m => m.Create()).Returns(() =>
			{
				return currentProductBasketMock.Object;
			});

			return productsBasketFactoryMock.Object;
		}
	}
}