using Market.CheckoutComponent.Interfaces;
using Market.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IProductBasketService productBasketService;

		public BasketController(IProductBasketService productBasketService)
		{
			this.productBasketService = productBasketService;
		}

		[HttpPost("{productName}")]
		public ActionResult AddProduct(string productName)
		{
			productBasketService.AddProduct(productName);
			return Ok();
		}

		[HttpGet]
		public ActionResult<string> Checkout()
		{
			var bill = productBasketService.Checkout();
			return bill;
		}

		[HttpPost("{productName}")]
		public void DecreaseUnits(string productName)
		{
			productBasketService.DecreaseUnits(productName);
		}
	}
}