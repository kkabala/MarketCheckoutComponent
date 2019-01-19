using Market.CheckoutComponent.Interfaces;
using Market.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IProductsBasket productsBasket;
		private readonly IDataService dataService;
		private readonly IProductBasketProviderService productBasketProviderService;

		public BasketController(IDataService dataService,
			IProductBasketProviderService productBasketProviderService)
		{
			this.dataService = dataService;
			this.productBasketProviderService = productBasketProviderService;

			productsBasket = productBasketProviderService.GetCurrent();
		}

		[HttpGet]
		public ActionResult<string> Checkout()
		{
			var bill = productsBasket.Checkout();
			productBasketProviderService.Reset();
			return bill.ToString();
		}

		[HttpPost("{productName}")]
		public ActionResult AddProduct(string productName)
		{
			var product = dataService.GetProductByName(productName);
			productsBasket.Add(product);
			return Ok();
		}

		[HttpPost("{productName}")]
		public void DecreaseUnits(string productName)
		{
			productsBasket.DecreaseUnits(productName);
		}
	}
}