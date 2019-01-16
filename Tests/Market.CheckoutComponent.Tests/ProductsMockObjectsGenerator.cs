using Market.CheckoutComponent.Model.Interfaces;
using Moq;

namespace Market.CheckoutComponent.Tests
{
	class ProductsMockObjectsGenerator
	{
		public IProduct Generate(string name =null ,decimal price = 0)
		{
			var mock = new Mock<IProduct>();
			mock.SetupGet(m => m.Name).Returns(name);
			mock.SetupGet(m => m.Price).Returns(price);
			return mock.Object;
		}
	}
}