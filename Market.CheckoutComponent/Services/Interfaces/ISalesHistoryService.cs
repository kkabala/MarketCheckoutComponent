using System.Collections.Generic;
using Market.CheckoutComponent.Model.Interfaces;

namespace Market.CheckoutComponent.Services.Interfaces
{
	public interface ISalesHistoryService
	{
		IEnumerable<IBill> GetAll();
		void Add(IBill bill);
	}
}
