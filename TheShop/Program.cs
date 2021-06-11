using System;
using System.Collections.Generic;
using TheShop.API;
using TheShop.DIContainer;
using Unity;
using System.Linq;

namespace TheShop
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			DependcyInjectionContainer containerService = new DependcyInjectionContainer();

			var shopService = containerService.Container.Resolve<IShopService>();
			List<ISupplier> supplierList = containerService.Container.ResolveAll<ISupplier>().ToList();

			try
			{
				//order and sell
				shopService.OrderAndSellArticle(1, 457, 10, supplierList);
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