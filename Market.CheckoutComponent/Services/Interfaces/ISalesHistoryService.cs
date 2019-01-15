using System.Collections.Generic;
using Market.CheckoutComponent.Model;

namespace Market.CheckoutComponent.Services.Interfaces
{
	public interface ISalesHistoryService
	{
		IEnumerable<Bill> GetAll();
		void Add(Bill bill);
	}
}
