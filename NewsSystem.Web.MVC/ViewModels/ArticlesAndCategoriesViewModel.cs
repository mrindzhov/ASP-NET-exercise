namespace NewsSystem.Web.MVC.ViewModels
{
    using Data.Models;
    using System.Linq;

    public class ArticlesAndCategoriesViewModel
    {
        public IQueryable<Article> Articles { get; set; }
        public IQueryable<Category> Categories { get; set; }
    }
}