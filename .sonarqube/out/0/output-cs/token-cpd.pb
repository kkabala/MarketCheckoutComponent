�
ZD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Interfaces\IProductsBasket.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #

Interfaces# -
{ 
public 
	interface 
IProductsBasket !
{ 
IBill 
Checkout 
( 
) 
; 
void 
Add 

(
 
IProduct 
product 
) 
; 
IProduct		 

[		
 
]		 
GetAll		
(		 
)		 
;		 
void

 
Remove

 
(


string

 
productsName

 !
)

! "
;

" #
void 

( 
string !
particularProductName 1
)1 2
;2 3
} 
}
RD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\BasicProduct.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
{ 
public 
class 
BasicProduct 
: 
IProduct %
{ 
public 
BasicProduct	 
( 
string 
name !
,! "
decimal# *
price+ 0
)0 1
{ 
Name		 
=		 	
name		
 
;		 
Price

 
=

	 

price

 
;

 
} 
public
string
Name
{
get
;
}
public 
decimal	 
Price 
{ 
get 
; 
} 
} 
} �O
JD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\Bill.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
{ 
public 
class 
Bill 
: 
IBill 
{		 
private

 	
const


 
string

 


 $
=

% &
$str

' 5
;

5 6
private 	
const
 
string 
PriceHeader "
=# $
$str% ,
;, -
private 	
const
 
string 

UnitHeader !
=" #
$str$ *
;* +
private
const
 
string
AmountHeader
=
$str
;
private 	
const
 
string "
ProductColumnFormatter -
=. /
$str0 :
;: ;
private 	
const
 
string  
PriceColumnFormatter +
=, -
$str. 7
;7 8
private 	
const
 
string 
UnitColumnFormatter *
=+ ,
$str- 5
;5 6
private 	
const
 
string !
AmountColumnFormatter ,
=- .
$str/ 7
;7 8
public 
Bill	 
(
IProduct 
[ 
] 
products !
,! "

[0 1
]1 2

)@ A
{ 
Products 
= 
products 
?? 
new 
IProduct &
[& '
]' (
{) *
}+ ,
;, -
DiscountsRules 
= 

??" $
new% (

[6 7
]7 8
{9 :
}; <
;< =
} 
public 
IProduct	 
[ 
] 
Products 
{ 
get "
;" #
}$ %
public 

[ 
] 
DiscountsRules '
{( )
get* -
;- .
}/ 0
public 
decimal	 
Total 
{ 
get 
{   
return!! 

Products!! 
.!! 
Sum!! 
(!! 
m!! 
=>!! 
m!! 
.!! 
Price!! $
)!!$ %
+!!& '
DiscountsRules!!( 6
.!!6 7
Sum!!7 :
(!!: ;
discount!!; C
=>!!D F
discount!!G O
.!!O P
	Calculate!!P Y
(!!Y Z
Products!!Z b
)!!b c
)!!c d
;!!d e
}"" 
}## 
public%% 
override%%	 
string%% 
ToString%% !
(%%! "
)%%" #
{&& 
var'' 

='' 
new'' 

(''( )
)'') *
;''* +
ApplyProductsHeader(( 
((( 

)(($ %
;((% &
ApplyProductsInfo)) 
()) 

)))" #
;))# $
ApplyDiscountsInfo** 
(** 

)**# $
;**$ %
ApplyTotalInfo++ 
(++ 

)++  
;++  !
return,, 	

 
.,, 
ToString,,  
(,,  !
),,! "
;,," #
}-- 
private// 	
void//
 
ApplyTotalInfo// 
(// 


)//9 :
{00 

.11 

AppendLine11 
(11 
)11 
;11 

.22 
Append22 
(22 
$"22 
Total: 22 !
{22! "
Total22" '
}22' (
"22( )
)22) *
;22* +
}33 
private55 	
void55
 
ApplyProductsHeader55 "
(55" #


)55> ?
{66 

.77 
Append77 
(77 
String77 
.77 
Format77 %
(77% &"
ProductColumnFormatter77& <
,77< =

)77K L
)77L M
;77M N

.88 
Append88 
(88 
String88 
.88 
Format88 %
(88% & 
PriceColumnFormatter88& :
,88: ;
PriceHeader88< G
)88G H
)88H I
;88I J

.99 
Append99 
(99 
String99 
.99 
Format99 %
(99% &
UnitColumnFormatter99& 9
,999 :

UnitHeader99; E
)99E F
)99F G
;99G H

.:: 
Append:: 
(:: 
String:: 
.:: 
Format:: %
(::% &!
AmountColumnFormatter::& ;
,::; <
AmountHeader::= I
)::I J
)::J K
;::K L

.;; 

AppendLine;; 
(;; 
);; 
;;; 
var<< 
headerLength<< 
=<< 

.<<# $
Length<<$ *
;<<* +

.== 
Append== 
(== 
$char== 
,== 
headerLength== )
)==) *
;==* +
}>> 
private@@ 	
void@@
 
ApplyProductsInfo@@  
(@@  !


)@@< =
{AA 
foreachBB 

(BB 
varBB 
singleProductGroupBB "
inBB# %
ProductsBB& .
.BB. /
GroupByBB/ 6
(BB6 7
mBB7 8
=>BB9 ;
mBB< =
.BB= >
NameBB> B
)BBB C
)BBC D
{CC 

.DD 

AppendLineDD 
(DD 
)DD 
;DD 
varEE 
productPriceEE 
=EE 
singleProductGroupEE )
.EE) *
FirstEE* /
(EE/ 0
)EE0 1
.EE1 2
PriceEE2 7
;EE7 8
varFF 
unitsFF 
=FF 
singleProductGroupFF "
.FF" #
CountFF# (
(FF( )
)FF) *
;FF* +

.GG 
AppendGG 
(GG 
StringGG 
.GG  
FormatGG  &
(GG& '"
ProductColumnFormatterGG' =
,GG= >
singleProductGroupGG? Q
.GGQ R
KeyGGR U
)GGU V
)GGV W
;GGW X

.HH 
AppendHH 
(HH 
StringHH 
.HH  
FormatHH  &
(HH& ' 
PriceColumnFormatterHH' ;
,HH; <
productPriceHH= I
.HHI J
ToStringHHJ R
(HHR S
$strHHS W
)HHW X
)HHX Y
)HHY Z
;HHZ [

.II 
AppendII 
(II 
StringII 
.II  
FormatII  &
(II& '
UnitColumnFormatterII' :
,II: ;
unitsII< A
)IIA B
)IIB C
;IIC D

.JJ 
AppendJJ 
(JJ 
StringJJ 
.JJ  
FormatJJ  &
(JJ& '!
AmountColumnFormatterJJ' <
,JJ< =
(JJ> ?
unitsJJ? D
*JJE F
productPriceJJG S
)JJS T
.JJT U
ToStringJJU ]
(JJ] ^
$strJJ^ b
)JJb c
)JJc d
)JJd e
;JJe f
}KK 

.LL 

AppendLineLL 
(LL 
)LL 
;LL 
}MM 
privateOO 	
voidOO
 
ApplyDiscountsInfoOO !
(OO! "


)OO= >
{PP 
ifQQ 
(QQ 
!QQ 
DiscountsRulesQQ 
.QQ 
AnyQQ 
(QQ 
)QQ 
)QQ 
{RR 
returnSS 

;SS
 
}TT 

.UU 

AppendLineUU 
(UU 
)UU 
;UU 

.VV 

AppendLineVV 
(VV 
$strVV 0
)VV0 1
;VV1 2
foreachXX 

(XX 
varXX 
singleDiscountXX 
inXX !
DiscountsRulesXX" 0
)XX0 1
{YY 
varZZ 

=ZZ 
singleDiscountZZ &
.ZZ& '
	CalculateZZ' 0
(ZZ0 1
ProductsZZ1 9
)ZZ9 :
;ZZ: ;
if[[ 
([[ 

>=[[ 
$num[[ 
)[[ 
{\\ 
continue]] 
;]]
}^^ 

.__ 
Append__ 
(__ 
singleDiscount__ '
.__' (
Name__( ,
)__, -
;__- .

.`` 
Append`` 
(`` 
$str`` 
)`` 
;`` 

.aa 

AppendLineaa 
(aa 

.aa* +
ToStringaa+ 3
(aa3 4
$straa4 8
)aa8 9
)aa9 :
;aa: ;
}bb 
}cc 
}dd 
}ee �
dD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\DiscountRules\BulkDiscountRule.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

{ 
public 
class 
BulkDiscountRule 
:  

{ 
private		 	
readonly		
 
string		 
productName		 %
;		% &
private

 	
readonly


 
int

 (
itemsRequiredToApplyDiscount

 3
;

3 4
private 	
readonly
 
decimal 
specialGroupPrice ,
;, -
public
BulkDiscountRule
(
string
name
,
string
productName
,
int
itemsRequiredToApplyDiscount
,
decimal
specialGroupPrice
)
{ 
Name 
= 	
name
 
; 
this 
. 
productName 
= 
productName !
;! "
this 
. (
itemsRequiredToApplyDiscount $
=% &(
itemsRequiredToApplyDiscount' C
;C D
this 
. 
specialGroupPrice 
= 
specialGroupPrice -
;- .
} 
public 
string	 
Name 
{ 
get 
; 
} 
public 
decimal	 
	Calculate 
( 
IEnumerable &
<& '
IProduct' /
>/ 0
products1 9
)9 :
{ 
var 
discountedProducts 
= 
products $
?$ %
.% &
Where& +
(+ ,
m, -
=>. 0
m1 2
.2 3
Name3 7
==8 :
productName; F
)F G
.G H
ToListH N
(N O
)O P
;P Q
if 
( 
discountedProducts 
== 
null !
||" $
discountedProducts% 7
.7 8
Count8 =
<> ?(
itemsRequiredToApplyDiscount@ \
)\ ]
{ 
return 

$num 
; 
} 
var   
numberOfDiscounts   
=   
discountedProducts   -
.  - .
Count  . 3
/  4 5(
itemsRequiredToApplyDiscount  6 R
;  R S
var"" %
discountPricesProductsSum""  
=""! "
numberOfDiscounts""# 4
*""5 6
specialGroupPrice""7 H
;""H I
var## 
regularPrice## 
=## 
discountedProducts## (
.##( )
First##) .
(##. /
)##/ 0
.##0 1
Price##1 6
;##6 7
var$$ 
regularPriceSum$$ 
=$$ 
discountedProducts$$ +
.$$+ ,
Count$$, 1
*$$2 3
regularPrice$$4 @
;$$@ A
var&& +
numberOfItemsWithoutTheDiscount&& &
=&&' (
discountedProducts&&) ;
.&&; <
Count&&< A
-&&B C
numberOfDiscounts&&D U
*&&V W(
itemsRequiredToApplyDiscount&&X t
;&&t u
var'' *
priceOfItemsWithoutTheDiscount'' %
=''& '+
numberOfItemsWithoutTheDiscount''( G
*''H I
regularPrice''J V
;''V W
return)) 	
())
 %
discountPricesProductsSum)) $
+))$ %*
priceOfItemsWithoutTheDiscount))& D
)))D E
-))F G
regularPriceSum))H W
;))W X
}** 
}++ 
},, �
gD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\DiscountRules\PackageDiscountRule.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

{ 
public 
class 
PackageDiscountRule !
:" #

{ 
public		 
string			 
Name		 
{		 
get		 
;		 
}		 
private

 	
readonly


 
decimal

 
discountAmount

 )
;

) *
private 	
readonly
 
string 
[ 
] 
packageProductNames /
;/ 0
public
PackageDiscountRule
(
string
name
,
decimal
discountAmount
,
params
string
[
]
packageProductNames
)
{ 
Name 
= 	
name
 
; 
this 
. 
discountAmount 
= 
discountAmount '
;' (
this 
. 
packageProductNames 
= 
packageProductNames 1
;1 2
} 
public 
decimal	 
	Calculate 
( 
IEnumerable &
<& '
IProduct' /
>/ 0
products1 9
)9 :
{ 
var 
groupedProducts 
= 
products !
?! "
." #
GroupBy# *
(* +
m+ ,
=>- /
m0 1
.1 2
Name2 6
)6 7
.7 8
ToList8 >
(> ?
)? @
;@ A
return 	
(
 
groupedProducts 
!= 
null "
&& 
packageProductNames 
. 
All 
( 
m  
=>! #
groupedProducts$ 3
.3 4
Any4 7
(7 8
p8 9
=>: <
p= >
.> ?
Key? B
==C E
mF G
)G H
)H I
)I J
? 
discountAmount 
* 
groupedProducts &
.& '
Select' -
(- .
m. /
=>0 2
m3 4
.4 5
Count5 :
(: ;
); <
)< =
.= >
Min> A
(A B
)B C
: 
$num 
; 	
} 
} 
} �
nD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\DiscountRules\PriceThresholdDiscountRule.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

{ 
public 
class 
PriceThresholdDiscountRule (
:) *

{ 
public		 
string			 
Name		 
{		 
get		 
;		 
}		 
private

 	
readonly


 
int

 
priceThreshold

 %
;

% &
private 	
readonly
 
int 
discountPercentage )
;) *
public
PriceThresholdDiscountRule
(
string
name
,
int
priceThreshold
,
int
discountPercentage
)
{ 
Name 
= 	
name
 
; 
this 
. 
priceThreshold 
= 
priceThreshold '
;' (
this 
. 
discountPercentage 
= 
discountPercentage /
;/ 0
} 
public 
decimal	 
	Calculate 
( 
IEnumerable &
<& '
IProduct' /
>/ 0
products1 9
)9 :
{ 
var 
productsSum 
= 
products 
? 
. 
Sum "
(" #
m# $
=>% '
m( )
.) *
Price* /
)/ 0
;0 1
return 	
(
 
productsSum 
. 
HasValue 
&&  "
productsSum# .
.. /
Value/ 4
>=5 7
priceThreshold8 F
)F G
? 
- 
( 
discountPercentage 
* 
productsSum (
.( )
Value) .
). /
/0 1
$num2 5
: 
$num 
; 
} 
} 
} �
VD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\Interfaces\IBill.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

Interfaces) 3
{ 
public 
	interface 
IBill 
{ 
IProduct 

[
 
] 
Products
{ 
get 
; 
} 

[ 
] 
DiscountsRules  
{! "
get# &
;& '
}( )
decimal 	
Total
 
{ 
get 
; 
} 
} 
}		 �
^D:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\Interfaces\IDiscountRule.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

Interfaces) 3
{ 
public 
	interface 

{ 
string 
Name	 
{ 
get 
; 
} 
decimal 	
	Calculate
 
( 
IEnumerable 
<  
IProduct  (
>( )
products* 2
)2 3
;3 4
}		 
}

 �
YD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Model\Interfaces\IProduct.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Model# (
.( )

Interfaces) 3
{ 
public 
	interface 
IProduct 
{ 
string 
Name	 
{ 
get 
; 
} 
decimal 	
Price
 
{ 
get 
; 
} 
} 
} � 
ND:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\ProductsBasket.cs
	namespace 	
Market
 
. 
CheckoutComponent "
{		 
public

 
class

 
ProductsBasket

 
:

 
IProductsBasket

 .
{ 
private 	
readonly
 
List 
< 
IProduct  
>  !
products" *
;* +
private
readonly
  
ISalesHistoryService
salesHistoryService
;
private 	
readonly
 )
IDiscountRulesProviderService 0(
discountRulesProviderService1 M
;M N
public 
ProductsBasket	 
(  
ISalesHistoryService ,
salesHistoryService- @
,@ A)
IDiscountRulesProviderService  (
discountRulesProviderService! =
)= >
{ 
this 
. 
salesHistoryService 
= 
salesHistoryService 1
;1 2
this 
. (
discountRulesProviderService $
=% &(
discountRulesProviderService' C
;C D
products 
= 
new 
List 
< 
IProduct 
>  
(  !
)! "
;" #
} 
public 
IBill	 
Checkout 
( 
) 
{ 
var 
bill 
= 
new 
Bill 
( 
products 
.  
ToArray  '
(' (
)( )
,) *(
discountRulesProviderService+ G
?G H
.H I
GetAllDiscountRulesI \
(\ ]
)] ^
)^ _
;_ `
salesHistoryService 
. 
Add 
( 
bill 
)  
;  !
return 	
bill
 
; 
} 
public 
void	 
Add 
( 
IProduct 
product "
)" #
{   
if!! 
(!! 
product!! 
!=!! 
null!! 
)!! 
products"" 
."" 
Add""
("" 
product"" 
)"" 
;"" 
}## 
public%% 
IProduct%%	 
[%% 
]%% 
GetAll%% 
(%% 
)%% 
{&& 
return'' 	
products''
 
.'' 
ToArray'' 
('' 
)'' 
;'' 
}(( 
public** 
void**	 
Remove** 
(** 
string** 
productsName** (
)**( )
{++ 
var,, 
productsToBeRemoved,, 
=,, 
products,, %
.,,% &
Where,,& +
(,,+ ,
m,,, -
=>,,. 0
m,,1 2
.,,2 3
Name,,3 7
==,,8 :
productsName,,; G
),,G H
.,,H I
ToList,,I O
(,,O P
),,P Q
;,,Q R
foreach.. 

(.. 
var.. 
product.. 
in.. 
productsToBeRemoved.. .
)... /
products// 
.// 
Remove//
(// 
product// 
)// 
;// 
}00 
public22 
void22	 

(22 
string22 "!
particularProductName22# 8
)228 9
{33 
var44 
productToBeRemoved44 
=44 
products44 $
.44$ %
FirstOrDefault44% 3
(443 4
m444 5
=>446 8
m449 :
.44: ;
Name44; ?
==44@ B!
particularProductName44C X
)44X Y
;44Y Z
products55 
.55 
Remove55 
(55 
productToBeRemoved55 %
)55% &
;55& '
}66 
}77 
}88 �
qD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Services\Interfaces\IDiscountRulesProviderService.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Services# +
.+ ,

Interfaces, 6
{ 
public 
	interface )
IDiscountRulesProviderService /
{ 

[ 
] 
GetAllDiscountRules %
(% &
)& '
;' (
} 
}		 �
hD:\Projects\MarketCheckoutComponent\Market.CheckoutComponent\Services\Interfaces\ISalesHistoryService.cs
	namespace 	
Market
 
. 
CheckoutComponent "
." #
Services# +
.+ ,

Interfaces, 6
{ 
public 
	interface  
ISalesHistoryService &
{ 
IEnumerable 
<
IBill 
> 
GetAll 
( 
) 
; 
void		 
Add		 

(		
 
IBill		 
bill		 
)		 
;		 
}

 
} 