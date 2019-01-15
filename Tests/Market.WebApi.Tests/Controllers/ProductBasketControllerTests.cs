using FluentAssertions;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Infrastructure.Model;
using Market.WebApi.Controllers;
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
