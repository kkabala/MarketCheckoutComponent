<strong>Market Checkout Component </strong>

This project is just a sample solution of code kata  with requirements are as below:

	- The component should be a library providing checkout functionality which calculates the total price of products provided
	- Products can be added incrementally
	- There are a number of predefined discounts rules which are applied when some condition is met:
		- buying x number of a particular product 
		- while buying two or more selected, different products,
		- when the total price of order exceeds some amount of money threshold
	- Details of discount rules above are defined by an external client app which uses the library (do not implement the external client app)
	- Adding new discount rules should be as easy as possible
	- Adding new product types should be straightforward,
	- There should be a possibility to get the data about sold products and applied discounts
	- There should be a possibility to print bills to clients (in text form list of all products with the prices and applied discounts)
	- There is a possibility to store in memory the history about sold products and discounts applied
	- The library should not use any external storage/interaction sources (like db, file, web service)
	- The library should be created using TDD approach
	
	- OPTIONAL: Create rest web api which exposes adding products & checkout functionalities
