using System;
using System.Collections.Generic;
using TheShop.Database;
using TheShop.Domain;
using TheShop.Loger;
using TheShop.Model.Domin;
using TheShop.Service;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			List<ISupplire> supplires = new List<ISupplire>() { new Supplier1(), new Supplier2(), new Supplier3() };						
			ShopService shopService = new ShopService(new DatabaseDriver(), new Logger(), supplires);

			try
			{
				Func<Article,bool>  articlePriceLessThen = a => a.ArticlePrice <= 458;
				//order and sell
				shopService.OrderAndSellArticle(1, articlePriceLessThen, 10);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			try
			{
				//print article on console
				var article = shopService.GetById(1);
				Console.WriteLine("Found article with ID: " + article.ID);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Article not found: " + ex);
			}

			try
			{
				//print article on console				
				var article = shopService.GetById(12);
				Console.WriteLine("Found article with ID: " + article.ID);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Article not found: " + ex);
			}

			Console.ReadKey();
		}
	}
}