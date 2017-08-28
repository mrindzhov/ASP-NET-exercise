namespace NewsSystem.Web.MVC.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Models;
    using Data.Services.Contracts;
    using Microsoft.AspNet.Identity;
    using System;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ViewModels;
    using PagedList;
    using System.Diagnostics;
    using Data;
    using Data.Repositories;
    using Data.Services;

    public class ArticlesController : Controller
    {
        public IArticlesServices ArticlesServices { get; set; }
        public ICategoriesServices CategoriesServices { get; set; }
        public ILikesServices LikesServices { get; set; } =
            new LikesService(new GenericRepository<Like>(new NewsSystemDbContext()));

        public ArticlesController(IArticlesServices articleService, ICategoriesServices categoriesServices)
        {
            this.ArticlesServices = articleService;
            this.CategoriesServices = categoriesServices;
        }
        //, string searchString,
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.LikeSortParm = sortOrder == "Likes" ? "likes_desc" : "Likes";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Article> articles = this.ArticlesServices.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "Likes":
                    articles = articles.OrderBy(s => s.Likes.Where(l => l.Value == true).Count());
                    break;
                case "likes_desc":
                    articles = articles.OrderByDescending(s => s.Likes.Where(l => l.Value == true).Count());
                    break;
                case "Category":
                    articles = articles.OrderBy(s => s.Category.Name);
                    break;
                case "category_desc":
                    articles = articles.OrderByDescending(s => s.Category.Name);
                    break;
                case "Date":
                    articles = articles.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    articles = articles.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }
            MapperConfiguration mc = new MapperConfiguration(cfg => { cfg.CreateMap<Article, ArticleViewModel>(); cfg.CreateMap<Category, CategoryViewModel>(); });
            IQueryable<ArticleViewModel> articlesVM = articles.ProjectTo<ArticleViewModel>(mc);

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(articlesVM.ToPagedList(pageNumber, pageSize));
        }

        // GET: Articles/Details/52

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = this.ArticlesServices.GetById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Like(int articledId)
        {
            string userId = this.User.Identity.GetUserId();
            Like like = this.LikesServices.GetByAuthorIdAndArticleId(userId, articledId);

            if (like == null)
            {
                Like createdLike = new Like()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    ArticleId = articledId,
                    Value = true
                };
                this.LikesServices.Like(createdLike);
            }
            else
            {
                this.LikesServices.ChangeLike(this.User.Identity.GetUserId(), articledId);
            }


            return RedirectToAction("Details", "Articles", new { id = articledId });
        }

        // GET: Articles/Create 
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.User.Identity.GetUserId(), "Id", "UserName");
            ViewBag.CategoryId = new SelectList(this.CategoriesServices.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,Title,DateCreated,AuthorId,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.AuthorId = this.User.Identity.GetUserId();
                article.DateCreated = DateTime.Now;
                this.ArticlesServices.Create(article);
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.ArticlesServices.GetAll(), "Id", "UserName", article.AuthorId);
            ViewBag.CategoryId = new SelectList(this.ArticlesServices.GetAll(), "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = this.ArticlesServices.GetById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AuthorId = new SelectList(this.ArticlesServices.GetAll(), "Id", "UserName", article.AuthorId);
            ViewBag.CategoryId = new SelectList(this.CategoriesServices.GetAll(), "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Title,DateCreated,AuthorId,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(article).State = EntityState.Modified;
                //db.SaveChanges();
                this.ArticlesServices.UpdateById(article.Id, article);
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.ArticlesServices.GetAll(), "Id", "UserName", article.AuthorId);
            ViewBag.CategoryId = new SelectList(this.ArticlesServices.GetAll(), "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = this.ArticlesServices.GetById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.ArticlesServices.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}
