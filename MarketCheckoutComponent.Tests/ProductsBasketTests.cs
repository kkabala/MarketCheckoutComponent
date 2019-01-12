using FluentAssertions;
using MarketCheckoutComponent.Model;
using NUnit.Framework;

namespace MarketCheckoutComponent.Tests
{
	public class ProductsBasketTests
	{
		private ProductsBasket productsBasket;

		[SetUp]
		public void Setup()
		{
			productsBasket = new ProductsBasket();
		}

		[TestCase(0)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(2)]
		[TestCase(1)]
		public void AddProduct_AddsNewEntryToTheBasket(int numberOfProducts)
		{
			for (int i = 0; i < numberOfProducts; i++)
			{
				productsBasket.AddProduct(new Product());
			}

			var bill = productsBasket.Checkout();

			bill.Products.Should().NotBeNull();
			bill.Products.Length.Should().Be(numberOfProducts);
		}

		[Test]
		public void Checkout_ReturnsBill()
		{
			var bill = productsBasket.Checkout();

			bill.Should().NotBeNull();
		}
	}
}