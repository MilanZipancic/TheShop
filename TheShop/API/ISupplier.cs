using TheShop.Model;

namespace TheShop.API
{
	public interface ISupplier
	{
		bool ArticleInInventory(int id);

		Article GetArticle(int id);
	}
}
