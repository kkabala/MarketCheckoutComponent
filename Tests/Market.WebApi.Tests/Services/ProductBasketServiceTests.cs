﻿using FluentAssertions;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Services;
using Moq;
using NUnit.Framework;

namespace Market.WebApi.Tests.Services
{
	[TestFixture]
	public class BasketProviderServiceTests
	{
		[Test]
		public void GetCurrent_ReturnsTheSameInstanceEveryTime()
		{
			//Arrange
			var salesHistoryMock = new Mock<ISalesHistoryService>();
			var service = new ProductBasketService(salesHistoryMock.Object, null);

			//Act
			var instance1 = service.GetCurrent();
			var instance2 = service.GetCurrent();
			var instance3 = service.GetCurrent();

			//Assert
			instance1.GetHashCode().Should().Be(instance2.GetHashCode());
			instance2.GetHashCode().Should().Be(instance3.GetHashCode());
			instance3.GetHashCode().Should().Be(instance1.GetHashCode());
		}

		[Test]
		public void Reset_ForcesCreatingNewBasketInsance_WhenGetCurrentIsCalled()
		{
			//Arrange
			var salesHistoryMock = new Mock<ISalesHistoryService>();
			var service = new ProductBasketService(salesHistoryMock.Object, null);

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
