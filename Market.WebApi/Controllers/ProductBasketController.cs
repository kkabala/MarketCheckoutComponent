using Market.CheckoutComponent;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBasketController : ControllerBase
    {
	    private static ProductsBasket productsBasket;
	    private readonly IDataService dataService;

		public ProductBasketController(IDataService dataService, ISalesHistoryService salesHistoryService)
		{
			this.dataService = dataService;
			if (productsBasket == null)
			{
				productsBasket = new ProductsBasket(salesHistoryService);
			}
		}

		[HttpGet]
		public ActionResult<Bill> Checkout()
		{
			var bill = productsBasket.Checkout();
			productsBasket = null;
			return bill;
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