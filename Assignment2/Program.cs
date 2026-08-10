using LINQ.DataSources;
using LINQ.Models;
using linq2;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
var productList = Source.ProductList;

//1.Get top 3 most expensive products
var top3Products = productList
    .OrderByDescending(p => p.UnitPrice)
    .Take(3);

foreach (var item in top3Products)
{
    Console.WriteLine(item);
}


Break();

//2. show page 2 of products, with page size = 5
int pageSize = 5;
int pageNumber = 2;

var productsPage2 = productList
    .Skip(pageSize * (pageNumber-1))
    .Take(pageSize);

foreach (var item in productsPage2)
{
    Console.WriteLine(item);
}

Break();

//3. Take products from the list as long as Their UnitPrice is less than
//$25(list is ordered by price).

var productsLessThan25 = productList
    .OrderBy( p => p.UnitPrice)
    .TakeWhile(p => p.UnitPrice < 25)
    .ToList();

foreach (var item in productsLessThan25)
{
    Console.WriteLine(item);
}

Break();

//4.Check if ALL products in the "Seafood" category are in stock

var allSeafoodInStock = productList
    .Where(p => p.Category.Equals("seafood", StringComparison.OrdinalIgnoreCase))
    .All(p => p.UnitsInStock > 0);

Console.WriteLine(allSeafoodInStock);

Break();

//5. Check if the ID list contains 9
int[] ids = { 3, 9, 13, 18 };

var containsNine = ids.Contains(9);
Console.WriteLine(containsNine);

Break();

//6. Group all products by Category and print each group
//with its product count.

var groupedProducts = productList
    .GroupBy(p => p.Category);

foreach (var group in groupedProducts)
{
    Console.WriteLine($"Category : {group.Key}");
    Console.WriteLine($"Count: {group.Count()}");
}

Break();

//7.Group products by Category and project only product
//names per group

var groupedProductNames = productList
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        ProductNames = g.Select(p => p.ProductName)
    });


foreach (var group in groupedProductNames)
{
    Console.WriteLine($"Category: {group.Category}");

    foreach (var productName in group.ProductNames)
    {
        Console.WriteLine($"  Name : {productName}");
    }

    Console.WriteLine();
}

Break();

//8. Find all categories that have MORE THAN 3 products

var categoriesMoreThan3Products = productList
    .GroupBy(p => p.Category)
    .Where(g => g.Count() > 3);

foreach (var group in categoriesMoreThan3Products)
{
    Console.WriteLine($"Category : {group.Key}");
}

Break();

//9.Using QUERY SYNTAX, group customers by Country, and for each
//group select { Country, Count, TotalOrderValue }.

var customerList = Source.CustomerList;

var groupedCustomers = from c in customerList
                       group c by c.Country into g
                       select new
                       {
                           Country = g.Key,
                           Count = g.Count(),
                           TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                       };

foreach (var group in groupedCustomers)
{
    Console.WriteLine($"Country Name : {group.Country} , " +
        $"Count : {group.Count} , TotalOrderValue {group.TotalOrderValue}");
}

Break();

//10.Calculate the total number of units in stock across all products

var totalUnitsInStock = productList
   .Sum(p => p.UnitsInStock) ;

Console.WriteLine(totalUnitsInStock);

Break();

//11. Find the CHEAPEST and MOST EXPENSIVE product prices

var cheapest = productList
    .MinBy(p => p.UnitPrice);

var mostExpensive = productList
    .MaxBy(p => p.UnitPrice);

Console.WriteLine(cheapest);
Console.WriteLine(mostExpensive);

Break();

//12. Get a distinct list of all product categories

var distinctCategories = productList
    .Select(p => p.Category)
    .Distinct();

foreach (var item in distinctCategories)
{
    Console.WriteLine(item);
}

Break();

//13.find product IDs that are in setA but NOT in setB
int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };

var productIds = setA.Except(setB);

var products = productList
    .Where(p => productIds.Contains(p.ProductID));

foreach (var item in products)
{
    Console.WriteLine(item);
}

Break();

//14. Find countries that appear in list1 but NOT in list2
//(case -insensitive).

string[] list1 = { "Germany", "France", "UK", "Spain" };

string[] list2 = { "france", "SPAIN", "Italy" };

var except = list1.Except(list2 , StringComparer.OrdinalIgnoreCase);

foreach (var item in except)
{
    Console.WriteLine(item);
}

Break();

//15. Build a Dictionary<int, Product> keyed by ProductID. Then
//retrieve and print the product with ID = 18.

var productById = productList
    .ToDictionary(p => p.ProductID);

Product product = productById[18];

Console.WriteLine(product);

Break();

//16. Get the first product whose price is greater than $50.

var firstProductOver50 = productList
    .First(p => p.UnitPrice > 50);

Console.WriteLine(firstProductOver50);

Break();

//17. Try to get the first product with a price > $500. it returns null
//instead of throwing.

var firstProductOver500 = productList
    .FirstOrDefault(p => p.UnitPrice > 500);

Console.WriteLine(firstProductOver500);

Break();

//18.Generate a multiplication table row for 7

var multiplicationTable7 = Enumerable.Range(1, 10)
    .Select(n => n * 7);

foreach (var num in multiplicationTable7)
    Console.WriteLine(num);

Break();

//19. Generate even numbers between 1 and 30.

var evenNumbers = Enumerable.Range(1, 30)
    .Where(n => n % 2==0);

foreach (var num in evenNumbers)
    Console.WriteLine(num);

Break();

//20. Concatenate the first 3 product names with the first 3
//customer company names into a single sequence.

var firstThreeProducts = productList
    .Take(3)
    .Select(p => p.ProductName);

var firstThreeCompanies = customerList
    .Take(3)
    .Select(c => c.CompanyName);

var firstThree = firstThreeProducts.Concat(firstThreeCompanies);

foreach (var item in firstThree)
{
    Console.WriteLine(item);
}

Break();

//21. Pair each product with a customer (by position) and produce
//a string "ProductName sold to CompanyName".

var pairs = firstThreeProducts.Zip(firstThreeCompanies,
    (productName, companyName) =>
            $"{productName} sold to {companyName}");

foreach (var item in pairs)
{
    Console.WriteLine(item);
}



void Break()
{
    Console.WriteLine();
    Console.WriteLine("--------------------------------------------------------------------------");
}