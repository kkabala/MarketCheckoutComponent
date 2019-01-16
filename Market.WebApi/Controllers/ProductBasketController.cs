using Market.CheckoutComponent.Interfaces;
using Market.WebApi.Services;
using Market.WebApi.Utilities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBasketController : ControllerBase
    {
	    private static IProductsBasket productsBasket;
	    private readonly IDataService dataService;

		public ProductBasketController(IDataService dataService, IProductsBasketFactory productsBasketFactory)
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

		[HttpPost]
	    public ActionResult Add(string productName)
	    {
		    var product = dataService.GetProductByName(productName);
			productsBasket.Add(product);
			return Ok();
	    }
	}
}