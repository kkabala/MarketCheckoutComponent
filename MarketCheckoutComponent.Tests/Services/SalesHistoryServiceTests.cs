using FluentAssertions;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests.Services
{
	[TestFixture]
	public class SalesHistoryServiceTests
	{
		[Test]
		public void Add_DoesNotAllowNulls()
		{
			//Arrange
			var salesHistoryService = new InMemorySalesHistoryService();

			//Act
			salesHistoryService.Add(null);

			//Assert
			salesHistoryService.GetAll().Should().BeEmpty();
		}

		[Test]
		public void Add_DoesNotAllowDuplicates()
		{
			//Arrange
			var salesHistoryService = new InMemorySalesHistoryService();
			var bill = new Bill(null, null);

			//Act
			salesHistoryService.Add(bill);
			salesHistoryService.Add(bill);

			//Assert
			salesHistoryService.GetAll().Should().OnlyHaveUniqueItems();
		}

		[Test]
		public void GetAll_ReturnsAllPreviouslyAddedBills()
		{
			//Arrange
			var salesHistoryService = new InMemorySalesHistoryService();
			var bills = new[]
			{
				new Bill(null, null),
				new Bill(null, null),
				new Bill(null, null),
				new Bill(null, null),
			};

			//Act
			foreach (var singleBill in bills)
				salesHistoryService.Add(singleBill);

			//Assert
			salesHistoryService.GetAll().Should().Equal(bills);
		}
	}
}
