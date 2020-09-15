using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheShop.Domain;
using TheShop.Loger;
using TheShop.Model.Domin;
using TheShop.Repository;

namespace TheShop.Service
{
    public class ShopService
	{
		private IArticlelRepository articlelRepository;
		private ILogger logger;
		private IEnumerable<ISupplire> suplires;		

		public ShopService(IArticlelRepository articlelRepository, ILogger logger, IEnumerable<ISupplire> suplires)
		{
			this.articlelRepository = articlelRepository;
			this.logger = logger;
			this.suplires = suplires;			
		}
		
		// Find articles in registered supplier, filter by criteria and sold		
		public void OrderAndSellArticle(int id,  Func<Article, bool> filter, int buyerId)
		{                      
            IEnumerable<Article> articles = FindArticlesInSupplires(id);
			IfAnyArticlesExists(articles);
          
			articles = FilterArticlesByCriteria(articles, filter );
			IfAnyArticlesExists(articles);
           
			Article article = GetMinPriceArticle(articles);
			SetArticleSellData(buyerId, article);
			SaveSoldArticle(article);
		}

		//check is any article exists and throw exception if not
		private void IfAnyArticlesExists(IEnumerable<Article> articles)
        {            
			if (!articles.Any())
				throw new Exception("Could not order article");
			
		}
		
		//save dolf article
        private void SaveSoldArticle(Article article)
        {
			try
			{
				articlelRepository.Save(article);
				logger.Info("Article with id=" + article.ID + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				logger.Error("Could not save article with id=" + article.ID);
				throw new Exception("Could not save article with id");
			}
			catch (Exception ex)
			{
				logger.Debug(ex.Message);
				throw new Exception("Could not save article with id");
			}
		}
		
		//filter articles by criteria
		private static IEnumerable<Article> FilterArticlesByCriteria(IEnumerable<Article> articles, Func<Article, bool> filter)
		{
			return articles.Where(a => filter(a));
		}
		
		//get article with min price
		private Article GetMinPriceArticle(IEnumerable<Article> articles)
        {
						
            return articles.First(a=>a.ArticlePrice.Equals((articles.Min(b => b.ArticlePrice))));
        }

		//get article with max price
		private Article GetMaxPriceArticle(IEnumerable<Article> articles)
		{

			return articles.First(a => a.ArticlePrice.Equals((articles.Max(b => b.ArticlePrice))));
		}
		
		//set article sell data
		private void SetArticleSellData(int buyerId, Article article)
        {
            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;
        }
		
		// find all articles in defined supplirs
		private IEnumerable<Article> FindArticlesInSupplires(int id)
		{
			try
			{
				return this.suplires.Where(s => s.ArticleInInventory(id))
									.Select(s =>s.GetArticle(id));
			}

			catch (Exception ex)
			{
				logger.Error(ex.Message);
				throw new System.Exception("Error occurred. Please try again later.");
			}
		}

        public Article GetById(int id)
		{
			return articlelRepository.GetById(id);
		}
	}

}
