using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.CheckoutComponent.Interfaces;

namespace Market.WebApi.Utilities.Interfaces
{
	public interface IProductsBasketFactory
	{
		IProductsBasket Create();
	}
}
