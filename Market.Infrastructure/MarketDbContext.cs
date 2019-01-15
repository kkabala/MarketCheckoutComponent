using System.Collections.Generic;
using Market.Infrastructure.Model;
using MarketCheckoutComponent.Model;
using Microsoft.EntityFrameworkCore;

namespace Market.Infrastructure
{
	public class MarketDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "MarketCheckoutComponentContext");
		}

		public virtual DbSet<Bill> Bills { get; set; }
		public virtual DbSet<Product> Products { get; set; }
	}
}
