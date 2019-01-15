using FluentAssertions;
using Market.Infrastructure.Model;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services.Interfaces;
using MarketWebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace MarketWebApi.Tests.Controllers
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
			var controller = new ProductBasketController(null, GetSalesHistoryServiceMock().Object);
			var product = new Product();

			//Act
			controller.Add(product);
			var result = controller.Checkout();

			//Assert
			result.Value.Should().NotBeNull();
			result.Value.Products.Should().Contain(product);
		}
	}
}
