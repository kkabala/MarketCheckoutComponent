﻿using System;
using System.Collections.Generic;
using System.Linq;
using Market.CheckoutComponent.Interfaces;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;

namespace Market.CheckoutComponent
{
	public class ProductsBasket : IProductsBasket
	{
		private readonly List<IProduct> products;
		private readonly ISalesHistoryService salesHistoryService;
		private readonly IDiscountRulesService discountRulesService;
		private readonly IProductDataService productDataService;

		public ProductsBasket(ISalesHistoryService salesHistoryService,
			IDiscountRulesService discountRulesService,
			IProductDataService productDataService)
		{
			this.salesHistoryService = salesHistoryService ?? throw new ArgumentNullException($"{nameof(salesHistoryService)}");
			this.discountRulesService = discountRulesService;
			this.productDataService = productDataService ?? throw new ArgumentNullException($"{nameof(productDataService)}");
			products = new List<IProduct>();
		}

		public IBill Checkout()
		{
			var bill = new Bill(products.ToArray(), discountRulesService?.GetAllDiscountRules());
			salesHistoryService.Add(bill);
			return bill;
		}

		public void Add(string productName)
		{
			IProduct product = productDataService.GetProductByName(productName);
			if (product!= null)
				products.Add(product);
		}

		public IProduct[] GetAllAdded()
		{
			return products.ToArray();
		}

		public void Remove(string productsName)
		{
			var productsToBeRemoved = products.Where(m => m.Name == productsName).ToList();

			foreach (var product in productsToBeRemoved)
				products.Remove(product);
		}

		public void DecreaseUnits(string particularProductName)
		{
			var productToBeRemoved = products.FirstOrDefault(m => m.Name == particularProductName);
			products.Remove(productToBeRemoved);
		}
	}
}