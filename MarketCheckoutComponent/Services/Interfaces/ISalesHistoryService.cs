using System.Collections.Generic;
using MarketCheckoutComponent.Model;

namespace MarketCheckoutComponent.Services.Interfaces
{
	public interface ISalesHistoryService
	{
		IEnumerable<Bill> GetAll();
		void Add(Bill bill);
	}
}
