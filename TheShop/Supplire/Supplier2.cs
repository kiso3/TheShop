using TheShop.Domain;
using TheShop.Model.Domin;

namespace TheShop
{
    public class Supplier2: ISupplire
	{
		public bool ArticleInInventory(int id)
		{
			return true;
		}

		public Article GetArticle(int id)
		{
			return new Article()
			{
				ID = 1,
				Name_of_article = "Article from supplier2",
				ArticlePrice = 459
			};
		}
	}

}
