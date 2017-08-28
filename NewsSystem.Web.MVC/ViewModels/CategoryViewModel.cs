namespace NewsSystem.Web.MVC.ViewModels
{
using System.Web.Mvc;
    public class CategoryViewModel
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}