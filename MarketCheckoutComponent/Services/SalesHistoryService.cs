using System.Collections.Generic;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services.Interfaces;

namespace MarketCheckoutComponent.Services
{
	public class SalesHistoryService : ISalesHistoryService
	{
		private readonly List<Bill> salesHistory;

		public SalesHistoryService()
		{
			salesHistory = new List<Bill>();
		}

		public void Add(Bill bill)
		{
			if (bill != null && !salesHistory.Contains(bill))
				salesHistory.Add(bill);
		}

		public IEnumerable<Bill> GetAll()
		{
			return salesHistory;
		}
	}
}