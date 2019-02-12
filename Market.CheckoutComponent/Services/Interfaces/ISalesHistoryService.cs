using Market.CheckoutComponent.Model.Interfaces;
using System.Collections.Generic;

namespace Market.CheckoutComponent.Services.Interfaces
{
	public interface ISalesHistoryService
	{
		void Add(IBill bill);

		IEnumerable<IBill> GetAll();
	}
}