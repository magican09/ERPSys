// See https://aka.ms/new-console-template for more information

using Catalog.Infrastructure;
using Catalogs.Domain.AggregateModel.CatalogAggregate;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;

using
    (var context =new  CatalogsContext())
{
    var catalog = new CatalogItem("cat_1");
    
   
    context.Add(catalog);
    context.SaveChanges();

}

Console.WriteLine("Hello, World!");