using Market.CheckoutComponent;
using Market.CheckoutComponent.Model;
using Market.CheckoutComponent.Model.Interfaces;
using Market.CheckoutComponent.Services.Interfaces;
using Market.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Market.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBasketController : ControllerBase
    {
	    private static ProductsBasket productsBasket;
	    private MarketDbContext context;

		public ProductBasketController(MarketDbContext context, ISalesHistoryService salesHistoryService)
		{
			this.context = context;
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
	    public ActionResult Add(IProduct product)
	    {
			//TODO: Db seeding with products+changing parameter to the primary key, not the whole product data
			productsBasket.Add(product);
		    return Ok();
	    }
	}
}