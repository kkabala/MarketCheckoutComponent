using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Market.WebApi.IntegrationTests
{
	public class BasketControllerTests
	{
		private WebApplicationFactory<Startup> factory;

		[SetUp]
		public void SetUp()
		{
			factory = new WebApplicationFactory<Startup>();
		}

		[TestCase("api/Basket/Checkout")]
		public async Task GetEndpoints_ReturnSuccess(string url)
		{
			//Arrange
			var client = factory.CreateClient();

			//Act
			var response = await client.GetAsync(url);

			//Assert
			response.EnsureSuccessStatusCode();
		}

		[TestCase("api/Basket/DecreaseUnits")]
		[TestCase("api/Basket/AddProduct/A")]
		public async Task PostEndpoints_ReturnSuccess(string url)
		{
			//Arrange
			var client = factory.CreateClient();

			//Act
			var response = await client.PostAsync(url, null);

			//Assert
			response.EnsureSuccessStatusCode();
		}
	}
}