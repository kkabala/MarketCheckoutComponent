using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Market.WebApi.IntegrationTests
{
	public class BasketControllerTests
	{
		private const string AddAProductAddress = BaseAddress + "/AddProduct/" + ProductName;
		private const string BaseAddress = "api/Basket";
		private const string CheckoutAddress = BaseAddress + "/Checkout";
		private const string DecreaseUnitsAddress = BaseAddress + "/DecreaseUnits";
		private const string DecreaseUnitsOfAProduct = DecreaseUnitsAddress + "/A";
		private const string ProductName = "A";
		private HttpClient client;

		[Test]
		public async Task Checkout_CantBeExecutedTwiceForTheSameBasket()
		{
			//Arrange

			//Act
			await client.PostAsync(AddAProductAddress, null);

			var checkoutResponse1 = await client.GetAsync(CheckoutAddress);
			checkoutResponse1.EnsureSuccessStatusCode();
			var billText1 = await checkoutResponse1.Content.ReadAsStringAsync();

			var checkoutResponse2 = await client.GetAsync(CheckoutAddress);
			checkoutResponse2.EnsureSuccessStatusCode();
			var billText2 = await checkoutResponse2.Content.ReadAsStringAsync();

			//Assert
			billText1.Should().NotBe(billText2);
		}

		[TestCase(5)]
		[TestCase(100)]
		public async Task Checkout_ReturnsBillWithAddedMinusDecreasedProducts(int productsToAdd)
		{
			//Arrange

			//Act
			for (int i = 0; i < productsToAdd; i++)
			{
				await client.PostAsync(AddAProductAddress, null);
			}

			await client.PostAsync(DecreaseUnitsOfAProduct, null);
			var checkoutResponse = await client.GetAsync(CheckoutAddress);

			//Assert
			checkoutResponse.EnsureSuccessStatusCode();
			var billText = await checkoutResponse.Content.ReadAsStringAsync();
			billText.Should().Contain(ProductName);
			billText.Should().Contain($" {(productsToAdd - 1)} ");
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(100)]
		public async Task Checkout_ReturnsBillWithAddedProducts(int productsToAdd)
		{
			//Arrange

			//Act
			for (int i = 0; i < productsToAdd; i++)
			{
				await client.PostAsync(AddAProductAddress, null);
			}

			var checkoutResponse = await client.GetAsync(CheckoutAddress);

			//Assert
			checkoutResponse.EnsureSuccessStatusCode();
			var billText = await checkoutResponse.Content.ReadAsStringAsync();
			billText.Should().Contain(ProductName);
			billText.Should().Contain(productsToAdd.ToString());
		}

		[Test]
		public async Task Checkout_ReturnsBillWithNoProducts_WhenThereWereExecutedEqualNumberOfAddAndDecreaseMethods()
		{
			//Arrange

			//Act
			await client.PostAsync(AddAProductAddress, null);
			await client.PostAsync(DecreaseUnitsOfAProduct, null);
			var checkoutResponse = await client.GetAsync(CheckoutAddress);

			//Assert
			checkoutResponse.EnsureSuccessStatusCode();
			var billText = await checkoutResponse.Content.ReadAsStringAsync();
			billText.Should().NotContain($" {ProductName} ");
		}

		[TestCase(CheckoutAddress)]
		public async Task GetEndpoints_ReturnSuccess(string url)
		{
			//Arrange

			//Act
			var response = await client.GetAsync(url);

			//Assert
			response.EnsureSuccessStatusCode();
		}

		[TestCase(DecreaseUnitsOfAProduct)]
		[TestCase(AddAProductAddress)]
		public async Task PostEndpoints_ReturnSuccess(string url)
		{
			//Arrange

			//Act
			var response = await client.PostAsync(url, null);

			//Assert
			response.EnsureSuccessStatusCode();
		}

		[SetUp]
		public void SetUp()
		{
			var factory = new WebApplicationFactory<Startup>();
			client = factory.CreateClient();
		}
	}
}