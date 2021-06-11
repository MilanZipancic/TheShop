using System;
using System.Collections.Generic;
using TheShop.API;
using TheShop.DataAccess;
using TheShop.Logging;
using TheShop.Model;

namespace TheShop
{
	public class ShopService : IShopService
	{
		private IDatabaseDriver _databaseDriver;
		private ILogger _logger;

		public ShopService(ILogger logger, IDatabaseDriver databaseDriver)
		{
			_databaseDriver = databaseDriver;
			_logger = logger;
		}

		//I guess client code should not be responsible for providing list of suppliers, but was eaiser for me to do this way
		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId, List<ISupplier> suppliers)
		{
			#region ordering article

			//I think this could be done with Chains of Responsibility pattern in a more gratifying way, but it would take me a time to implement it
			//Also, implementaion of buisness logic seemed to to be flawed, so I ve changed it to match give explaination how it should work (also it would make
			//sense to find a cheapest Article from all suppliers, but that was not part of buisness logic, if I understood correctly)
			Article article = null;
			foreach (var supplier in suppliers)
			{
				bool articleExists = supplier.ArticleInInventory(id);
				if (article == null && articleExists)
				{
					Article tempArticle = supplier.GetArticle(id);
					if (maxExpectedPrice >= tempArticle.ArticlePrice) article = tempArticle;
				}
			}

			#endregion

			#region selling article

			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			_logger.Debug("Trying to sell article with id=" + id);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;

			try
			{
				_databaseDriver.Save(article);
				_logger.Info("Article with id=" + id + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				_logger.Error("Could not save article with id=" + id);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}

			#endregion
		}

		public Article GetById(int id)
		{
			return _databaseDriver.GetById(id);
		}
	}
}
