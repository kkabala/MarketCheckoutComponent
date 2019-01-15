using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Market.Infrastructure.Utilities;
using MarketCheckoutComponent.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Market.Infrastructure.Tests
{
	[TestFixture]
	public class DatabaseSalesHistoryServiceTests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(10)]
		[TestCase(112)]
		public void Add_SavesBillToContextDbSet(int numberOfBillsToBeSaved)
		{
			//Arrange
			var contextMock = new Mock<MarketDbContext>();
			var billDbSetMock = new Mock<DbSet<Bill>>();
			billDbSetMock.Setup(m => m.Add(It.IsAny<Bill>()));

			contextMock.Setup(m => m.Bills).Returns(billDbSetMock.Object);
			var service = new DatabaseSalesHistoryService(contextMock.Object);

			//Act
			for (int i = 0; i < numberOfBillsToBeSaved; i++)
			{
				var bill = new Bill(null, null);
				service.Add(bill);
			}

			//Assert
			billDbSetMock.Verify(m => m.Add(It.IsAny<Bill>()), Times.Exactly(numberOfBillsToBeSaved));
		}

		[Test]
		public void GetAll_ReturnsAllBillsFromContext()
		{
			//Arrange
			var contextMock = new Mock<MarketDbContext>();
			var dbSetData = new List<Bill>(){new Bill(null,null), new Bill(null,null), new Bill(null,null)};
			var billDbSetMock = CreateDbSetMock(dbSetData);

			contextMock.Setup(m => m.Bills).Returns(billDbSetMock.Object);
			var service = new DatabaseSalesHistoryService(contextMock.Object);

			//Act
			var returnedBills = service.GetAll();

			//Assert
			returnedBills.Should().Contain(dbSetData);
		}

		private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
		{
			var elementsAsQueryable = elements.AsQueryable();
			var dbSetMock = new Mock<DbSet<T>>();

			dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

			return dbSetMock;
		}
	}
}