using LINQ.DataSources;
using LINQ.Models;
using System.Diagnostics.Metrics;
using System.Drawing;

//1. Get all products from the "Seafood" category. Print each
//product's name and price.

var productList = Source.ProductList;


var seafoodProducts = productList
    .Where(p => p.Category == "Seafood");

foreach (var item in seafoodProducts)
{
    Console.WriteLine($"Product Name : {item.ProductName} - Unit Price : {item.UnitPrice}");
}


Break();

//2. Get a list of only the product names from ProductList. Print
//each name.
var productNames = productList
   .Select(p => p.ProductName)
   .ToList();

foreach (var item in productNames)
{
    Console.WriteLine($"Product Name : {item}");
}

Break();

//3. Sort all products by UnitPrice (ascending). Print each
//product's name and price.

var sortedProducts = productList
   .OrderBy(p => p.UnitPrice);

foreach (var item in sortedProducts)
{
    Console.WriteLine($"Product Name : {item.ProductName} - Unit Price : {item.UnitPrice}");
}

Break();

//4. Get all products where UnitPrice is between 10 and 30
var customizedProduct = productList
   .Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);

foreach (var item in customizedProduct)
{
    Console.WriteLine($"Product Name : {item.ProductName} - Unit Price :{item.UnitPrice}");
}

Break();

//5. Get all products that are in stock (UnitsInStock > 0) and
//belong to the "Condiments" category.

var inStockProducts = productList
   .Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");

foreach (var item in inStockProducts)
{
    Console.WriteLine($"Product Name : {item.ProductName} - UnitsInStock :{item.UnitsInStock}" +
        $" - Category :{item.Category}");
}

Break();

/*6. Create a new anonymous type with three properties:
● Name → the product name
● Price → the unit price
● StockStatus → a string: "Available" if UnitsInStock > 0,
otherwise "Out of Stock"
● Print the result.*/

var productStatus = productList
   .Select(p => new
   {
       Name = p.ProductName,
       Price = p.UnitPrice,
       StockStatus = GetStockStatus(p.UnitsInStock)
   });

foreach (var item in productStatus)
{
    Console.WriteLine($"Product Name : {item.Name} - Unit Price :{item.Price}" +
        $" - Stock Status :{item.StockStatus}");
}

Break();

//7. Print each product's name along with its position (1-based)
//in the list. Expected format: 1.Chai, 2.Chang, etc.

var productPositions = productList
    .Select((p, i) => new
    {
        index = i + 1,
        Name = p.ProductName
    })
    .ToList();

foreach (var item in productPositions)
{
    Console.WriteLine($"{item.index}.{item.Name}");
}

Break ();

//8.Sort ProductList by Category ascending, then within each
//category, sort by UnitPrice descending.

var orderedProducts = productList
    .OrderBy(p => p.Category)
    .ThenByDescending(p => p.UnitPrice);

foreach (var item in orderedProducts)
{
    Console.WriteLine(item);
}

Break();

//9. Get all products from the "Beverages" category, sorted by
//UnitsInStock descending. Print name and stock.

var beveragesProducts = productList
    .Where(p=> p.Category == "Beverages")
    .OrderByDescending(p => p.UnitsInStock);

foreach (var item in beveragesProducts)
{
    Console.WriteLine($"Product Name : {item.ProductName} - Stock : {item.UnitsInStock}");
}

Break();

//10. Using QUERY SYNTAX with a compound from clause, list
//all orders placed in 1997 or later showing CustomerID and OrderDate.

var customers = Source.CustomerList;

var placedOrdered = from c in customers
                    from o in c.Orders
                    where o.OrderDate >= new DateTime(1997, 1, 1)
                    select new
                    {
                        c.CustomerID,
                        o.OrderDate
                    };

foreach (var item in placedOrdered)
{
    Console.WriteLine($" CustomerID : {item.CustomerID} - Order Date : {item.OrderDate}");
}

Break();

//11. Show position number alongside ProductName

var position =
    (from p in productList
     select p)
    .Select((p, i) => new
    {
        Index = i + 1,
        Name = p.ProductName
    });

foreach (var item in position)
{
    Console.WriteLine(item);
}


Break();

//12. Sort first by-word length and then by a
//case -insensitive sort of the words in an array.

string[] fruits =
{
    "aPPLE",
    "AbAcUs",
    "bRaNcH",
    "BlUeBeRrY",
    "ClOvEr",
    "cHeRrY"
};

var listedFruits = fruits
    .OrderBy(f => f.Length)
    .ThenBy(f => f, StringComparer.OrdinalIgnoreCase)
    .ToArray();

foreach (var item in listedFruits)
{
    Console.WriteLine(item);
}

Break();

//13. Create a list of all digits in the array whose second letter
//is 'i' that is reversed from the order in the original array.

string[] digits =
{
    "Zero",
    "One",
    "Two",
    "Three",
    "Four",
    "Five",
    "Six",
    "Seven",
    "Eight",
    "Nine"
};

var listedDigits = digits
    .Where(d => d[1] == 'i' || d[1] == 'I')
    .Reverse()
    .ToList();

foreach (var item in listedDigits)
{
    Console.WriteLine(item);
}



// used in code 
static string GetStockStatus(int unitsInStock)   //=> Q6
{
    return unitsInStock > 0 ? "Available" : "Out of Stock";
}

void Break()
{
    Console.WriteLine();
    Console.WriteLine("--------------------------------------------------------------------------");
}