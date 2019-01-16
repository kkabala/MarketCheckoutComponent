using System.Collections.Generic;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;

namespace Market.WebApi.Services
{
	public class InMemorySalesHistoryService : ISalesHistoryService
	{
		private readonly List<IBill> historicalBills = new List<IBill>();
		public IEnumerable<IBill> GetAll()
		{
			return historicalBills;
		}

		public void Add(IBill bill)
		{
			if (!historicalBills.Contains(bill) && bill != null)
				historicalBills.Add(bill);
		}
	}
}