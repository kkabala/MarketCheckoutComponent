using System;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Services;
using Market.Services.Interfaces;
using Market.Services.Utilities;
using Market.Services.Utilities.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(120);
				options.Cookie.HttpOnly = true;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddTransient<ISalesHistoryService, InMemorySalesHistoryService>();
			services.AddTransient<IProductDataService, SampleProductDataService>();
			services.AddTransient<IDiscountRulesService, SampleProductDataService>();
			services.AddTransient<IProductsBasketFactory, ProductsBasketFactory>();

			//For simplicity the api handles one user at a time
			services.AddSingleton<IProductBasketService, ProductBasketService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}