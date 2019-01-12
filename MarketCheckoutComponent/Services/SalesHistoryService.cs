using MarketCheckoutComponent.Infrastructure;
using MarketCheckoutComponent.Services.Interfaces;

namespace MarketCheckoutComponent.Services
{
	public class SalesHistoryService : ISalesHistoryService
	{
		private MarketCheckoutComponentContext context;

		public SalesHistoryService()
		{
			context = new MarketCheckoutComponentContext();
		}
	}

}