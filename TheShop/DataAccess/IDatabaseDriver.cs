using TheShop.Model;

namespace TheShop.DataAccess
{
	public interface IDatabaseDriver
	{
		Article GetById(int id);
		void Save(Article article);
	}
}
