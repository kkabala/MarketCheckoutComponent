ý
QD:\Projects\MarketCheckoutComponent\Market.WebApi\Controllers\BasketController.cs
	namespace 	
Market
 
. 
WebApi 
. 
Controllers #
{ 
[ 
Route 

(
 
$str &
)& '
]' (
[		 
ApiController		 
]		 
public

 

class

 
BasketController

 !
:

" #
ControllerBase

$ 2
{ 
private 
static 
IProductsBasket #
productsBasket$ 2
;2 3
private 
readonly 
IDataService "
dataService# .
;. /
public 
BasketController	 
( 
IDataService &
dataService' 2
,2 3"
IProductsBasketFactory4 J!
productsBasketFactoryK `
)` a
{ 
this 
. 
dataService 
= 
dataService !
;! "
if 
( 
productsBasket 
== 
null 
) 
{ 
productsBasket 
= !
productsBasketFactory *
.* +
Create+ 1
(1 2
)2 3
;3 4
} 
} 
[ 
HttpGet 

]
 
public 
ActionResult	 
< 
string 
> 
Checkout &
(& '
)' (
{ 
var 
bill 
= 
productsBasket 
. 
Checkout %
(% &
)& '
;' (
productsBasket 
= 
null 
; 
return 	
bill
 
. 
ToString 
( 
) 
; 
}   
["" 
HttpPost"" 
("" 
$str"" 
)"" 
]"" 
public## 
ActionResult## 

AddProduct## #
(### $
string##$ *
productName##+ 6
)##6 7
{$$ 
var%% 	
product%%
 
=%% 
dataService%% 
.%%  
GetProductByName%%  0
(%%0 1
productName%%1 <
)%%< =
;%%= >
productsBasket&& 
.&& 
Add&& 
(&& 
product&& 
)&& 
;&& 
return'' 	
Ok''
 
('' 
)'' 
;'' 
}(( 
[** 
HttpPost** 
(** 
$str** 
)** 
]** 
public++ 
void++ 
DecreaseUnits++ 
(++ 
string++ %
productName++& 1
)++1 2
{,, 
productsBasket-- 
.-- 
DecreaseUnits-- 
(--  
productName--  +
)--+ ,
;--, -
}.. 
}// 
}00 ÿ
<D:\Projects\MarketCheckoutComponent\Market.WebApi\Program.cs
	namespace 	
Market
 
. 
WebApi 
{ 
public 
class 
Program 
{ 
public 
static	 
void 
Main 
( 
string  
[  !
]! "
args# '
)' (
{		  
CreateWebHostBuilder

 
(

 
args

 
)

 
.

 
Build

 #
(

# $
)

$ %
.

% &
Run

& )
(

) *
)

* +
;

+ ,
} 
public 
static	 
IWebHostBuilder  
CreateWebHostBuilder  4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
WebHost 

.
  
CreateDefaultBuilder 
(  
args  $
)$ %
. 

UseStartup 
< 
Startup 
> 
( 
) 
; 
} 
} •
YD:\Projects\MarketCheckoutComponent\Market.WebApi\Services\InMemorySalesHistoryService.cs
	namespace 	
Market
 
. 
WebApi 
. 
Services  
{ 
public 
class '
InMemorySalesHistoryService )
:* + 
ISalesHistoryService, @
{ 
private		 	
readonly		
 
List		 
<		 
IBill		 
>		 
historicalBills		 .
=		/ 0
new		1 4
List		5 9
<		9 :
IBill		: ?
>		? @
(		@ A
)		A B
;		B C
public

 
IEnumerable

	 
<

 
IBill

 
>

 
GetAll

 "
(

" #
)

# $
{ 
return 	
historicalBills
 
; 
} 
public 
void	 
Add 
( 
IBill 
bill 
) 
{ 
if 
( 
! 
historicalBills 
. 
Contains  
(  !
bill! %
)% &
&&' )
bill* .
!=/ 1
null2 6
)6 7
historicalBills 
. 
Add 
( 
bill 
) 
; 
} 
} 
} ó
UD:\Projects\MarketCheckoutComponent\Market.WebApi\Services\Interfaces\IDataService.cs
	namespace 	
Market
 
. 
WebApi 
. 
Services  
.  !

Interfaces! +
{ 
public 
	interface 
IDataService 
{ 
IProduct 

GetProductByName 
( 
string "
name# '
)' (
;( )
} 
}		 Ñ
OD:\Projects\MarketCheckoutComponent\Market.WebApi\Services\SampleDataService.cs
	namespace		 	
Market		
 
.		 
WebApi		 
.		 
Services		  
{

 
public 
class 
SampleDataService 
:  !
IDataService" .
,. /)
IDiscountRulesProviderService0 M
{ 
private 	
readonly
 
List 
< 
IProduct  
>  !
products" *
=+ ,
new- 0
List1 5
<5 6
IProduct6 >
>> ?
(? @
)@ A
{ 
new 
BasicProduct 
( 
$str 
, 
$num 
) 
, 
new 
BasicProduct 
( 
$str 
, 
$num 
) 
, 
new 
BasicProduct 
( 
$str 
, 
$num 
) 
, 
new 
BasicProduct 
( 
$str 
, 
$num 
) 
, 
} 
; 
public 
IProduct	 
GetProductByName "
(" #
string# )
name* .
). /
{ 
return 	
products
 
. 
SingleOrDefault "
(" #
m# $
=>% '
m( )
.) *
Name* .
==/ 1
name2 6
)6 7
;7 8
} 
public 
IDiscountRule	 
[ 
] 
GetAllDiscountRules ,
(, -
)- .
{ 
return 	
new
 
IDiscountRule 
[ 
] 
{ 
new 
BulkDiscountRule 
( 
$str *
,* +
$str, /
,/ 0
$num1 2
,2 3
$num4 6
)6 7
,7 8
new 
BulkDiscountRule 
( 
$str *
,* +
$str, /
,/ 0
$num1 2
,2 3
$num4 6
)6 7
,7 8
new   
BulkDiscountRule   
(   
$str   *
,  * +
$str  , /
,  / 0
$num  1 2
,  2 3
$num  4 6
)  6 7
,  7 8
new!! 
BulkDiscountRule!! 
(!! 
$str!! *
,!!* +
$str!!, /
,!!/ 0
$num!!1 2
,!!2 3
$num!!4 6
)!!6 7
,!!7 8
new## 
PackageDiscountRule## 
(## 
$str## 1
,##1 2
-##3 4
$num##4 6
,##6 7
$str##8 ;
,##; <
$str##= @
)##@ A
,##A B
new$$ 
PackageDiscountRule$$ 
($$ 
$str$$ 3
,$$3 4
-$$5 6
$num$$6 8
,$$8 9
$str$$: =
,$$= >
$str$$? B
,$$B C
$str$$D G
,$$G H
$str$$I L
)$$L M
,$$M N
new&& &
PriceThresholdDiscountRule&& "
(&&" #
$str&&# )
,&&) *
$num&&+ .
,&&. /
$num&&0 2
)&&2 3
}'' 
;'' 
}(( 
})) 
}** ‹
<D:\Projects\MarketCheckoutComponent\Market.WebApi\Startup.cs
	namespace 	
Market
 
. 
WebApi 
{ 
public 
class 
Startup 
{ 
public 
Startup	 
( 
IConfiguration 
configuration  -
)- .
{ 
Configuration 
= 
configuration  
;  !
} 
public 
IConfiguration	 
Configuration %
{& '
get( +
;+ ,
}- .
public 
void	 
ConfigureServices 
(  
IServiceCollection  2
services3 ;
); <
{ 
services 
. %
AddDistributedMemoryCache %
(% &
)& '
;' (
services 
. 

AddSession 
( 
options 
=> !
{ 
options 
. 
IdleTimeout 
= 
TimeSpan "
." #
FromSeconds# .
(. /
$num/ 2
)2 3
;3 4
options   
.   
Cookie   
.   
HttpOnly   
=   
true   "
;  " #
}!! 
)!! 
;!! 
services"" 
."" 
AddMvc"" 
("" 
)"" 
."" #
SetCompatibilityVersion"" ,
("", - 
CompatibilityVersion""- A
.""A B
Version_2_1""B M
)""M N
;""N O
services$$ 
.$$ 
AddTransient$$ 
<$$  
ISalesHistoryService$$ -
,$$- .'
InMemorySalesHistoryService$$/ J
>$$J K
($$K L
)$$L M
;$$M N
services%% 
.%% 
AddTransient%% 
<%% 
IDataService%% %
,%%% &
SampleDataService%%' 8
>%%8 9
(%%9 :
)%%: ;
;%%; <
services&& 
.&& 
AddTransient&& 
<&& )
IDiscountRulesProviderService&& 6
,&&6 7
SampleDataService&&8 I
>&&I J
(&&J K
)&&K L
;&&L M
services'' 
.'' 
AddTransient'' 
<'' "
IProductsBasketFactory'' /
,''/ 0!
ProductsBasketFactory''1 F
>''F G
(''G H
)''H I
;''I J
}(( 
public++ 
void++	 
	Configure++ 
(++ 
IApplicationBuilder++ +
app++, /
,++/ 0
IHostingEnvironment++1 D
env++E H
)++H I
{,, 
if-- 
(-- 
env-- 

.--
 
IsDevelopment-- 
(-- 
)-- 
)-- 
{.. 
app// 
.// %
UseDeveloperExceptionPage// !
(//! "
)//" #
;//# $
}00 
else11 
{22 
app33 
.33 
UseHsts33 
(33 
)33 
;33 
}44 
app66 
.66 
UseHttpsRedirection66 
(66 
)66 
;66 
app77 
.77 
UseMvc77 
(77 
)77 
;77 
}88 
}99 
}:: à
`D:\Projects\MarketCheckoutComponent\Market.WebApi\Utilities\Interfaces\IProductsBasketFactory.cs
	namespace 	
Market
 
. 
WebApi 
. 
	Utilities !
.! "

Interfaces" ,
{ 
public 
	interface "
IProductsBasketFactory (
{ 
IProductsBasket 
Create 
( 
) 
; 
} 
}		 €
TD:\Projects\MarketCheckoutComponent\Market.WebApi\Utilities\ProductsBasketFactory.cs
	namespace 	
Market
 
. 
WebApi 
. 
	Utilities !
{ 
public 
class !
ProductsBasketFactory #
:$ %"
IProductsBasketFactory& <
{		 
private

 	
readonly


  
ISalesHistoryService

 '
salesHistoryService

( ;
;

; <
private 	
readonly
 )
IDiscountRulesProviderService 0(
discountRulesProviderService1 M
;M N
public !
ProductsBasketFactory	 
(  
ISalesHistoryService 3
salesHistoryService4 G
,G H)
IDiscountRulesProviderService  (
discountRulesProviderService! =
)= >
{ 
this 
. 
salesHistoryService 
= 
salesHistoryService 1
;1 2
this 
. (
discountRulesProviderService $
=% &(
discountRulesProviderService' C
;C D
} 
public 
IProductsBasket	 
Create 
(  
)  !
{ 
return 	
new
 
ProductsBasket 
( 
salesHistoryService 0
,0 1(
discountRulesProviderService2 N
)N O
;O P
} 
} 
} 