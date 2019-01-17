using Market.CheckoutComponent.Interfaces;
using Market.WebApi.Services.Interfaces;
using Market.WebApi.Utilities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
	    private static IProductsBasket productsBasket;
	    private readonly IDataService dataService;

		public BasketController(IDataService dataService, IProductsBasketFactory productsBasketFactory)
		{
			this.dataService = dataService;

			if (productsBasket == null)
			{
				//For simplicity the api handles one user at a time
				productsBasket = productsBasketFactory.Create();
			}
		}

		[HttpGet]
		public ActionResult<string> Checkout()
		{
			var bill = productsBasket.Checkout();
			productsBasket = null;
			return bill.ToString();
		}

		[HttpPost("{productName}")]
	    public ActionResult AddProduct(string productName)
	    {
		    var product = dataService.GetProductByName(productName);
			productsBasket.Add(product);
			return Ok();
	    }

		[HttpPost]
	    public void DecreaseUnits(string productName)
		{
			productsBasket.DecreaseUnits(productName);
		}
    }
}