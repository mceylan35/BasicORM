// See https://aka.ms/new-console-template for more information
using BasicORM;
using BasicORM.Console;
using BasicORM.Enum;

Console.WriteLine("Hello, World!");

#region Products tablosunda Name' de a harfi geçen ve stock 40 dan büyük olan ürünleri listeleyen sorgu

Query<Product> query = new Query<Product>("Products");
query.Add(Criteria<Product>.Contains("Name", "a", QueryLogicalOperator.And));//name alanında a harfi geçen
query.Add(Criteria<Product>.GreaterThan("Stock", 40));
string _query=query.GenerateWhereClause();
Console.WriteLine("Oluşan Sorgu=",_query);

#endregion

#region Product' Price 10'dan büyük ise ve name de çanta geçenleri listeleyen sql sorgu 
Query<Product> query2 = new Query<Product>(p => new
{
    p.Id,
    p.Name
},"Products");
query2.Add(Criteria<Product>.GreaterThanOrEqueal(w => w.Price, 10, QueryLogicalOperator.Or));
query2.Add(Criteria<Product>.Equal(p => p.Name, "Çanta", QueryLogicalOperator.None));
string _query2=query2.GenerateWhereClause();
#endregion
