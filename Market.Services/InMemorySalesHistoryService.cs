using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using System.Collections.Generic;

namespace Market.Services
{
	public class InMemorySalesHistoryService : ISalesHistoryService
	{
		private readonly List<IBill> historicalBills = new List<IBill>();

		public void Add(IBill bill)
		{
			if (!historicalBills.Contains(bill) && bill != null)
			{
				historicalBills.Add(bill);
			}
		}

		public IEnumerable<IBill> GetAll()
		{
			return historicalBills;
		}
	}
}