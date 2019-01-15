using MarketCheckoutComponent.Model;
using MarketCheckoutComponent.Services.Interfaces;
using System.Collections.Generic;

namespace Market.Infrastructure.Utilities
{
	public class DatabaseSalesHistoryService : ISalesHistoryService
	{
		private readonly MarketDbContext context;

		public DatabaseSalesHistoryService(MarketDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<Bill> GetAll()
		{
			return context.Bills;
		}

		public void Add(Bill bill)
		{
			context.Bills.Add(bill);
		}
	}
}