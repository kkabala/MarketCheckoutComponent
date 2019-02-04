using Market.CheckoutComponent.Interfaces;
using Market.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IProductsBasket productsBasket;
		private readonly IProductDataService productDataService;
		private readonly IProductBasketService productBasketService;

		public BasketController(IProductDataService productDataService,
			IProductBasketService productBasketService)
		{
			this.productDataService = productDataService;
			this.productBasketService = productBasketService;

			productsBasket = productBasketService.GetCurrent();
		}

		[HttpGet]
		public ActionResult<string> Checkout()
		{
			var bill = productsBasket.Checkout();
			productBasketService.Reset();
			return bill.ToString();
		}

		[HttpPost("{productName}")]
		public ActionResult AddProduct(string productName)
		{
			productsBasket.Add(productName);
			return Ok();
		}

		[HttpPost("{productName}")]
		public void DecreaseUnits(string productName)
		{
			productsBasket.DecreaseUnits(productName);
		}
	}
}