using FluentAssertions;
using Market.CheckoutComponent.Model.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Market.Services.Tests.Services
{
	[TestFixture]
	public class InMemorySalesHistoryServiceTests
	{
		[Test]
		public void Add_CannotAddNulls()
		{
			//Arrange
			var service = new InMemorySalesHistoryService();

			//Act
			service.Add(null);
			var allBills = service.GetAll();

			//Assert
			allBills.Count().Should().Be(0);
		}

		[Test]
		public void Add_CannotAddSameBillTwice()
		{
			//Arrange
			var service = new InMemorySalesHistoryService();
			var bill = GetDefaultTestingBill();

			//Act
			service.Add(bill);
			service.Add(bill);
			var allBills = service.GetAll();

			//Assert
			allBills.Count().Should().Be(1);
			allBills.Should().Contain(bill);
		}

		[Test]
		public void GetAll_ReturnsPreviouslyAddedBills()
		{
			//Arrange
			var service = new InMemorySalesHistoryService();
			var bill = GetDefaultTestingBill();

			//Act
			service.Add(bill);
			var allBills = service.GetAll();

			//Assert
			allBills.Count().Should().Be(1);
			allBills.Should().Contain(bill);
		}

		private IBill GetDefaultTestingBill()
		{
			var products = new[]
			{
				GetMockedProduct("A", 40),
				GetMockedProduct("A", 40),
				GetMockedProduct("B", 92),
				GetMockedProduct("B", 92),
				GetMockedProduct("C", 11)
			};
			var discounts = new[]
			{
				GetMockedDiscountRule("Christmas discount", -10),
				GetMockedDiscountRule("Holiday discount", -7),
			};

			var bill = GetMockedBill(products, discounts);
			return bill;
		}

		private IBill GetMockedBill(IProduct[] products, IDiscountRule[] discounts)
		{
			var mock = new Mock<IBill>();
			mock.Setup(m => m.Products).Returns(products);
			mock.Setup(m => m.DiscountsRules).Returns(discounts);
			return mock.Object;
		}

		private IDiscountRule GetMockedDiscountRule(string name, int discountAmount)
		{
			var mock = new Mock<IDiscountRule>();
			mock.Setup(m => m.Name).Returns(name);
			mock.Setup(m => m.Calculate(It.IsAny<IProduct[]>())).Returns(discountAmount);
			return mock.Object;
		}

		private IProduct GetMockedProduct(string name, decimal price)
		{
			var mock = new Mock<IProduct>();
			mock.Setup(m => m.Name).Returns(name);
			mock.Setup(m => m.Price).Returns(price);
			return mock.Object;
		}
	}
}