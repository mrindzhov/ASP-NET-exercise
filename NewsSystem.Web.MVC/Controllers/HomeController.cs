namespace NewsSystem.Web.MVC.Controllers
{
    using Data.Models;
    using Data.Services.Contracts;
    using ViewModels;
    using Ninject;
    using System.Linq;
    using System.Web.ModelBinding;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public IArticlesServices ArticlesServices { get; set; }
        public ICategoriesServices CategoriesServices { get; set; }
        public HomeController(IArticlesServices articleService, ICategoriesServices categoriesService)
        {
            this.CategoriesServices = categoriesService;
            this.ArticlesServices = articleService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult News()
        {
            var articles = this.ArticlesServices.GetTop(3);
            var categories = this.CategoriesServices.GetAll();
            int a = articles.Count();
            ArticlesAndCategoriesViewModel model = new ArticlesAndCategoriesViewModel()
            {
                Articles = articles,
                Categories = categories
            };
            int b = categories.Count();

            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Article([QueryString]int id)
        {
            Article article = this.ArticlesServices.GetById(id);
            return View(article);
        }
    }
}