using System.Linq;
using FluentAssertions;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Market.CheckoutComponent.Tests
{
	public class ProductsBasketTests
	{
		private ProductsMockObjectsGenerator productsGenerator;

		[SetUp]
		public void SetUp()
		{
			this.productsGenerator = new ProductsMockObjectsGenerator();
		}

		[TestCase(0)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(2)]
		[TestCase(1)]
		public void Add_AddsNewEntryToTheBasket(int numberOfProducts)
		{
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			for (int i = 0; i < numberOfProducts; i++)
			{
				productsBasket.Add(productsGenerator.Generate());
			}

			var products = productsBasket.GetAll();

			//Assert
			products.Should().NotBeNull();
			products.Length.Should().Be(numberOfProducts);
		}

		[Test]
		public void Add_DoesNotAddAnyItemsWhenNullIsPassed()
		{
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			productsBasket.Add(null);
			var products = productsBasket.GetAll();

			//Assert
			products.Should().NotBeNull();
			products.Length.Should().Be(0);
		}

		[Test]
		public void Checkout_ReturnsBill()
		{
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.Should().NotBeNull();
		}

		[Test]
		public void Checkout_SavesBillToSalesHistory()
		{
			//Arrange
			var salesHistoryServiceMock = GetSalesHistoryServiceMockWithActions();
			var productsBasket = new ProductsBasket(salesHistoryServiceMock.Object);

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			salesHistoryServiceMock.Verify(m => m.Add(It.IsAny<Bill>()), Times.Once);
			bill.Should().NotBeNull();
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMockWithNoSetup()
		{
			return new Mock<ISalesHistoryService>();
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMockWithActions()
		{
			var mock = new Mock<ISalesHistoryService>();
			mock.Setup(m => m.Add(It.IsAny<Bill>()));
			return mock;
		}

		[Test]
		public void GetAll_ReturnsPreviouslyAddedProducts()
		{
			//Arrange
			var products = new[]
			{
				productsGenerator.Generate("testB", 1),
				productsGenerator.Generate("SampleProduct", 4),
				productsGenerator.Generate("aabbcc", 10)
			};
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length);
			addedProducts.Should().Contain(products);
		}

		[Test]
		public void GetAll_ReturnsEmptyArray_WhenNoProductsWereAdded()
		{
			//Arrange
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(0);
		}

		[Test]
		public void Remove_RemovesOnlyProductWithNameProvided()
		{
			//Arrange
			string particularProductName = "ProductA";
			var products = new[]
			{
				productsGenerator.Generate("testB", 1),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate("aabbcc", 10)
			};
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length - 1);
			addedProducts.Should().NotContain(particularProductName);
		}

		[Test]
		public void Remove_RemovesAllProductsWithNameProvided()
		{
			//Arrange
			string particularProductName = "ProductA";
			var products = new[]
			{
				productsGenerator.Generate("testB", 1),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate("aabbcc", 10)
			};
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length - products.Count(m=>m.Name==particularProductName));
			addedProducts.Should().NotContain(particularProductName);
		}

		[TestCase("ProductA")]
		[TestCase("")]
		[TestCase(null)]
		public void Remove_DoesNotRemoveAnything_WhenProductWereNotAddedPreviously(string particularProductName)
		{
			//Arrange
			var products = new[] { productsGenerator.Generate("testB", 1), productsGenerator.Generate("aabbcc", 10) };
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length);
			addedProducts.Should().NotContain(m => m.Name == particularProductName);
		}

		[Test]
		public void DecreaseUnits_RemovesOneUnitOfTheProductWithNameProvided()
		{
			//Arrange
			string particularProductName = "ProductA";
			var products = new[]
			{
				productsGenerator.Generate("testB", 1),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate(particularProductName, 4),
				productsGenerator.Generate("aabbcc", 10)
			};
			var particularProductsUnits = products.Count(m => m.Name == particularProductName);
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.DecreaseUnits(particularProductName);
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length - 1);
			addedProducts.Count(m => m.Name == particularProductName).Should().Be(particularProductsUnits-1);
		}


		[TestCase("ProductA")]
		[TestCase("")]
		[TestCase(null)]
		public void DecreaseUnits_DoesNotRemoveAnything_WhenNoProductWithNameProvidedIsAdded(string particularProductName)
		{
			//Arrange
			var products = new[] { productsGenerator.Generate("testB", 1), productsGenerator.Generate("aabbcc", 10) };
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceMockWithNoSetup().Object);

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.DecreaseUnits(particularProductName);
			var addedProducts = productsBasket.GetAll();

			//Assert
			addedProducts.Length.Should().Be(products.Length);
			addedProducts.Should().NotContain(m => m.Name == particularProductName);
		}
	}
}