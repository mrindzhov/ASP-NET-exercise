namespace NewsSystem.Web.MVC.ViewModels
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ArticleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public Category Category { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}