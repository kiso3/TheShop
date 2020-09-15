using System.Collections.Generic;
using System.Linq;
using TheShop.Domain;
using TheShop.Model.Domin;
using TheShop.Repository;

namespace TheShop.Database
{
    //in memory implementation
    public class DatabaseDriver: IArticlelRepository
	{
		private List<Article> _articles = new List<Article>();

		public Article GetById(int id)
		{
            return _articles.Single(x => x.ID == id);
		}

		public void Save(Article article)
		{
			_articles.Add(article);
		}
	}

}
