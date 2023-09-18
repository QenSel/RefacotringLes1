using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesBlog_Core;
using VivesBlog_Models;

namespace VivesBlog_Services.Services
{
	public class ArticleService
	{

		private readonly BlogDbContext _dbContext;

		public ArticleService(BlogDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IList<Article> GetAll()
		{
			return _dbContext.Articles.
				Include(a => a.Author).
				ToList();
		}





		public Article GetArticle(int id)
		{
			return _dbContext.Articles.Find(id);
		}

		public Article Create(Article article)
		{
			_dbContext.Articles.Add(article);
			_dbContext.SaveChanges();
			return article;
		}

		public Article Update(Article article, int id)
		{
			var dbArticle = _dbContext.Articles.Find(id);
			if (dbArticle != null)
			{
				return null;
			}

			dbArticle.Author = article.Author;
			dbArticle.Title = article.Title;
			dbArticle.Description = article.Description;
			dbArticle.Content = article.Content;
			dbArticle.CreatedDate = article.CreatedDate;
		

			_dbContext.SaveChanges();
			return article;


		}

		public Article Delete(int id)
		{
			var article = new Article
			{
				Id = id,
				Author = new Person(),
				Title = string.Empty,
				Description = string.Empty,
				Content = string.Empty,
				CreatedDate = DateTime.Now,

			

			};

			_dbContext.Articles.Attach(article);
			_dbContext.Articles.Remove(article);

			_dbContext.SaveChanges();

			return article;
		}


	}
}
