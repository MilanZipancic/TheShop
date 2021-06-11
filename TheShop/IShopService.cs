using System.Collections.Generic;
using TheShop.API;
using TheShop.Model;

namespace TheShop
{
	public interface IShopService
	{
		void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId, List<ISupplier> suppliers);

		Article GetById(int id);
	}
}
