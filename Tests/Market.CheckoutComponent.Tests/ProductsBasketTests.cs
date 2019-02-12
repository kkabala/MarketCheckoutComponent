using FluentAssertions;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace Market.CheckoutComponent.Tests
{
	public class ProductsBasketTests
	{
		private ProductsMockObjectsGenerator productsGenerator;

		[TestCase(0)]
		[TestCase(5)]
		[TestCase(10)]
		[TestCase(2)]
		[TestCase(1)]
		public void Add_AddsNewEntryToTheBasket(int numberOfProducts)
		{
			//Arrange
			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			for (int i = 0; i < numberOfProducts; i++)
			{
				productsBasket.Add(productsGenerator.Generate().Name);
			}

			var products = productsBasket.GetAllAdded();

			//Assert
			products.Should().NotBeNull();
			products.Length.Should().Be(numberOfProducts);
		}

		[Test]
		public void Add_DoesNotAddAnyItemsWhenNullIsPassed()
		{
			//Arrange
			var productsBasket = GetProductBasketWithNoSetup();

			//Act
			productsBasket.Add(null);
			var products = productsBasket.GetAllAdded();

			//Assert
			products.Should().NotBeNull();
			products.Length.Should().Be(0);
		}

		[Test]
		public void ArgumentExceptionIsThrown_WhenNoDataServiceIsPassedToTheConstructor()
		{
			//Arrange
			var discountRulesService = new Mock<IDiscountRulesService>().Object;
			var salesHistoryProvider = new Mock<ISalesHistoryService>().Object;

			//Act
			Action constructorAction = () => new ProductsBasket(salesHistoryProvider, discountRulesService, null);

			//Assert
			constructorAction.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void ArgumentExceptionIsThrown_WhenNoSalesHistoryServiceIsPassedToTheConstructor()
		{
			//Arrange
			var discountRulesService = new Mock<IDiscountRulesService>();

			//Act
			Action constructorAction = () => new ProductsBasket(null, discountRulesService.Object, null);

			//Assert
			constructorAction.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void Checkout_ContainsAllDiscountsFromProvider()
		{
			//Arrange
			var discountsProviderMock = new Mock<IDiscountRulesService>();
			var discountRules = new[]
			{
				new Mock<IDiscountRule>().Object,
				new Mock<IDiscountRule>().Object,
				new Mock<IDiscountRule>().Object
			};

			discountsProviderMock.Setup(m => m.GetAllDiscountRules()).Returns(discountRules);
			var productsBasket = new ProductsBasket(GetSalesHistoryServiceWithNoSetup(), discountsProviderMock.Object, GetDataServiceWithNoSetup());

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.DiscountsRules.Should().BeEquivalentTo(discountRules);
		}

		[Test]
		public void Checkout_ReturnsBill()
		{
			//Arrange
			var productsBasket = GetProductBasketWithNoSetup();

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.Should().NotBeNull();
		}

		[Test]
		public void Checkout_ReturnsBillWithNoDiscounts_WhenNoDiscountRulesProviderServiceIsPassed()
		{
			//Arrange

			var productsBasket = GetProductBasketWithNoSetup();

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			bill.DiscountsRules.Length.Should().Be(0);
		}

		[Test]
		public void Checkout_SavesBillToSalesHistory()
		{
			//Arrange
			var salesHistoryServiceMock = GetSalesHistoryServiceMockWithActions();
			var productsBasket = new ProductsBasket(salesHistoryServiceMock.Object, null, GetDataServiceWithNoSetup());

			//Act
			var bill = productsBasket.Checkout();

			//Assert
			salesHistoryServiceMock.Verify(m => m.Add(It.IsAny<Bill>()), Times.Once);
			bill.Should().NotBeNull();
		}

		[TestCase("ProductA")]
		[TestCase("")]
		[TestCase(null)]
		public void DecreaseUnits_DoesNotRemoveAnything_WhenNoProductWithNameProvidedIsAdded(string particularProductName)
		{
			//Arrange
			var products = new[]
			{
				"testB",
				"aabbcc"
			};
			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.DecreaseUnits(particularProductName);
			var addedProducts = productsBasket.GetAllAdded();

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
				"testB",
				particularProductName,
				particularProductName,
				particularProductName,
				particularProductName,
				particularProductName,
				"aabbcc"
			};
			var particularProductsUnits = products.Count(m => m == particularProductName);
			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.DecreaseUnits(particularProductName);
			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(products.Length - 1);
			addedProducts.Count(m => m.Name == particularProductName).Should().Be(particularProductsUnits - 1);
		}

		[Test]
		public void GetAll_ReturnsEmptyArray_WhenNoProductsWereAdded()
		{
			//Arrange
			var productsBasket = GetProductBasketWithNoSetup();

			//Act
			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(0);
		}

		[Test]
		public void GetAll_ReturnsPreviouslyAddedProducts()
		{
			//Arrange
			var products = new[]
			{
				"testB",
				"SampleProduct",
				"aabbcc"
			};

			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(products.Length);
		}

		[TestCase("ProductA")]
		[TestCase("")]
		[TestCase(null)]
		public void Remove_DoesNotRemoveAnything_WhenNoProductsWereAddedPreviously(string particularProductName)
		{
			//Arrange
			var products = new[]
			{
				"testB",
				"aabbcc"
			};

			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(products.Length);
			addedProducts.Should().NotContain(m => m.Name == particularProductName);
		}

		[Test]
		public void Remove_RemovesAllProductsWithNameProvided()
		{
			//Arrange
			string particularProductName = "ProductA";
			var products = new[]
			{
				"testB",
				particularProductName,
				particularProductName,
				"aabbcc",
			};
			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(products.Length - products.Count(m => m == particularProductName));
			addedProducts.Should().NotContain(particularProductName);
		}

		[Test]
		public void Remove_RemovesOnlyProductWithNameProvided()
		{
			//Arrange
			string particularProductName = "ProductA";
			var products = new[]
			{
				"testB",
				particularProductName,
				"aabbcc"
			};
			var productsBasket = GetProductBasketWithMockedDataService();

			//Act
			foreach (var singleProduct in products)
			{
				productsBasket.Add(singleProduct);
			}

			productsBasket.Remove(particularProductName);
			var addedProducts = productsBasket.GetAllAdded();

			//Assert
			addedProducts.Length.Should().Be(products.Length - 1);
			addedProducts.Should().NotContain(particularProductName);
		}

		[SetUp]
		public void SetUp()
		{
			productsGenerator = new ProductsMockObjectsGenerator();
		}

		private IProductDataService GenerateMockedDataService()
		{
			var mock = new Mock<IProductDataService>();
			mock.Setup(m => m.GetProductByName(It.IsAny<string>())).Returns<string>((m) =>
			{
				var productMock = new Mock<IProduct>();
				productMock.Setup(k => k.Name).Returns(m);
				var random = new Random();
				productMock.Setup(k => k.Price).Returns(random.Next(1, 1000));
				return productMock.Object;
			});

			return mock.Object;
		}

		private IProductDataService GetDataServiceWithNoSetup()
		{
			return new Mock<IProductDataService>().Object;
		}

		private ProductsBasket GetProductBasketWithMockedDataService()
		{
			return new ProductsBasket(GetSalesHistoryServiceWithNoSetup(), null, GenerateMockedDataService());
		}

		private ProductsBasket GetProductBasketWithNoSetup()
		{
			return new ProductsBasket(GetSalesHistoryServiceWithNoSetup(), null, GetDataServiceWithNoSetup());
		}

		private Mock<ISalesHistoryService> GetSalesHistoryServiceMockWithActions()
		{
			var mock = new Mock<ISalesHistoryService>();
			mock.Setup(m => m.Add(It.IsAny<Bill>()));
			return mock;
		}

		private ISalesHistoryService GetSalesHistoryServiceWithNoSetup()
		{
			return new Mock<ISalesHistoryService>().Object;
		}
	}
}