﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Market.WebApi
{
	public class Program
	{
		protected Program()
		{
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}
	}
}