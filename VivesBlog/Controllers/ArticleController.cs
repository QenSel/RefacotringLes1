using Microsoft.AspNetCore.Mvc;
using VivesBlog_Models;
using VivesBlog_Services;
using VivesBlog_Services.Services;

namespace VivesBlog.Ui.MVC.Controllers
{
	
	public class ArticleController : Controller
	{
		private readonly ArticleService _articleservice;

		public ArticleController(ArticleService articleservice)
		{
			_articleservice = articleservice;
		}
		
		public IActionResult Index()
		{
			var Articles = _articleservice.GetAll();

			return View(Articles);
		}
		public IActionResult Details(int id)
		{
			var article = _articleservice.GetArticle(id);

			return View(article);
		}


	}
}
