using Microsoft.EntityFrameworkCore;

namespace MarketCheckoutComponent.Infrastructure
{
	public class MarketCheckoutComponentContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "MarketCheckoutComponentContext");
		}
	}
}