namespace NewsSystem.Web.MVC.Controllers
{
    using Data.Models;
    using Data.Services.Contracts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Ninject;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;

    public class PrivateController : Controller
    {
        [Inject]
        public IArticlesServices ArticlesServices { get; set; }
        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }
        // GET: Private
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categories()
        {
            var categories = this.CategoriesServices.GetAll();
            return View(categories);
        }

        //[HttpPost]
        //public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        //{
        //    var categories = this.CategoriesServices.GetAll().ToDataSourceResult(request);
        //    return this.Json(categories);
        //}
        [HttpPost]
        public ActionResult ReadCategories([DataSourceRequest]DataSourceRequest request)
        {
            var categories = this.CategoriesServices.GetAll().ToDataSourceResult(request);
            return this.Json(categories);
        }
       
        [HttpPost]
        public ActionResult CreateCategory([DataSourceRequest]DataSourceRequest request, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.CategoriesServices.Create(model.Name);
            }
            return View();
        }
        public ActionResult Articles()
        {
            IQueryable<Article> articles = this.ArticlesServices.GetAll();
            return View(articles);
        }
        [HttpPost]
        public ActionResult ReadArticles([DataSourceRequest]DataSourceRequest request)
        {
            var articles = this.ArticlesServices.GetAll().ToDataSourceResult(request);
            return this.Json(articles);
        }
        [HttpPost]
        public ActionResult CreateArticle([DataSourceRequest]DataSourceRequest request, Article model)
        {
            if (!ModelState.IsValid)
            {
                this.ArticlesServices.Create(model);
            }
            return View();
        }

    }
}