using System.Collections.Generic;
using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services.Interfaces;

namespace MarketCheckoutComponent.Services
{
	public class SalesHistoryService : ISalesHistoryService
	{
		private List<Bill> salesHistory;

		public SalesHistoryService()
		{
			salesHistory = new List<Bill>();
		}
	}
}