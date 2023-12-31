﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VivesBlog_Core;
using VivesBlog_Models;

namespace VivesBlog.Ui.MVC
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext _database;

        public HomeController()
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseInMemoryDatabase("VivesBlog");
            _database = new BlogDbContext(builder.Options);
            if (!_database.Articles.Any())
            {
                _database.Seed();
            }
        }


        public IActionResult Index()
        {
            var articles = _database.Articles
                .Include(a => a.Author)
                .ToList();
            return View(articles);
        }

        public IActionResult Details(int id)
        {
            var article = _database.Articles
                .Include(a => a.Author)
                .SingleOrDefault(a => a.Id == id);

            return View(article);
        }

        [HttpGet("People/Index")]
        public IActionResult PeopleIndex()
        {
            var people = _database.People.ToList();
            return View(people);
        }

        [HttpGet("People/Create")]
        public IActionResult PeopleCreate()
        {
            return View();
        }

        [HttpPost("People/Create")]
        public IActionResult PeopleCreate(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _database.People.Add(person);
            _database.SaveChanges();

            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Edit/{id}")]
        public IActionResult PeopleEdit(int id)
        {
            var person = _database.People.Single(p => p.Id == id);

            return View(person);
        }

        [HttpPost("People/Edit/{id}")]
        public IActionResult PeopleEdit(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _database.People.Single(p => p.Id == person.Id);

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;

            _database.SaveChanges();

            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("People/Delete/{id}")]
        public IActionResult PeopleDelete(int id)
        {
            var person = _database.People.Single(p => p.Id == id);

            return View(person);
        }

        [HttpPost("People/Delete/{id}")]
        public IActionResult PeopleDeleteConfirmed(int id)
        {
            var dbPerson = _database.People.Single(p => p.Id == id);

            _database.People.Remove(dbPerson);

            _database.SaveChanges();

            return RedirectToAction("PeopleIndex");
        }

        [HttpGet("Blog/Index")]
        public IActionResult BlogIndex()
        {
            var articles = _database.Articles
                .Include(a => a.Author)
                .ToList();
            return View(articles);
        }

        [HttpGet("Blog/Create")]
        public IActionResult BlogCreate()
        {
            var articleModel = CreateArticleModel();

            return View(articleModel);
        }

        [HttpPost("Blog/Create")]
        public IActionResult BlogCreate(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = CreateArticleModel(article);
                return View(articleModel);
            }

            article.CreatedDate = DateTime.Now;

            _database.Articles.Add(article);

            _database.SaveChanges();

            return RedirectToAction("BlogIndex");
        }

        [HttpGet("Blog/Edit/{id}")]
        public IActionResult BlogEdit(int id)
        {
            var article = _database.Articles.Single(p => p.Id == id);

            var articleModel = CreateArticleModel(article);

            return View(articleModel);
        }

        [HttpPost("Blog/Edit/{id}")]
        public IActionResult BlogEdit(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = CreateArticleModel(article);
                return View(articleModel);
            }

            var dbArticle = _database.Articles.Single(p => p.Id == article.Id);

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;
            dbArticle.AuthorId = article.AuthorId;

            _database.SaveChanges();

            return RedirectToAction("BlogIndex");
        }

        [HttpGet("Blog/Delete/{id}")]
        public IActionResult BlogDelete(int id)
        {
            var article = _database.Articles
                .Include(a => a.Author)
                .Single(p => p.Id == id);

            return View(article);
        }

        [HttpPost("Blog/Delete/{id}")]
        public IActionResult BlogDeleteConfirmed(int id)
        {
            var dbArticle = _database.Articles.Single(p => p.Id == id);

            _database.Articles.Remove(dbArticle);

            _database.SaveChanges();

            return RedirectToAction("BlogIndex");
        }

        private ArticleModel CreateArticleModel(Article article = null)
        {
            article ??= new Article();

            var authors = _database.People
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            var articleModel = new ArticleModel
            {
                Article = article,
                Authors = authors
            };

            return articleModel;
        }
    }

}


