<strong>Market Checkout Component </strong>

This project is just a sample solution of code kata  with requirements as below:

	- The component should be a library providing checkout functionality which calculates the total price of products provided
	- Products can be added incrementally
	- Names of products are unique (there are no more than 1 product type with a particular name)
	- All added products with the name specified can be removed at once
	- Products unit amount can be decreased (if user choosed 2 units of the product, he can remove 1 unit)
	- There are a number of predefined discounts rules which are applied when some condition is met:
		- buying x number of a particular product 
		- while buying two or more selected, different products,
		- when the total price of order exceeds some amount of money threshold
	- Details of discount rules above (like which exactly products take part in a discount or what is the value of threshold) are defined by an external client app which uses the library (do not implement the external client app)
	- Adding new discount rules should be as easy as possible
	- Adding new product types should be straightforward,
	- There should be a possibility to get the data about already sold products and applied discounts (which can be stored in memory, but replacing it with any other storage, like a database, should be pretty easy from the architectural level)
	- There should be a possibility to print bills to clients (in text form list of all products with the prices, units bought, units*price amount and applied discounts)
	- The library should not use any external storage/interaction sources (like db, file, web service)
	- The library should be created using TDD approach
	
	- OPTIONAL: Create web api which exposes adding products & checkout functionalities. Don't forget to create discounts from discount rules created in the previous step (with any values you like).
