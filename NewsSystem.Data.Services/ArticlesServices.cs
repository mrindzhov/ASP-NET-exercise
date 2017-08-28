namespace NewsSystem.Data.Services
{
    using Models;
    using Repositories;
    using Contracts;
    using System.Linq;
    using System.Data.Entity;

    public class ArticlesServices : IArticlesServices
    {
        private IRepository<Article> articles;
        public ArticlesServices(IRepository<Article> articles)
        {
            this.articles = articles;
        }

        public void Create(Article articleToInsert)
        {
            this.articles.Add(articleToInsert);
            this.articles.SaveChanges();
        }

        public void DeleteById(int id)
        {
            this.articles.Delete(id);
            this.articles.SaveChanges();
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All().Include(s => s.Category);
        }

        public Article GetById(int id)
        {
            return this.articles.GetById(id);
        }

        public IQueryable<Article> GetTop(int count)
        {
            return this.articles.All().OrderByDescending(a => a.Likes.Where(l => l.Value == true).Count()).Take(count);
        }

        public void UpdateById(int id, Article article)
        {
            var articleToUpdate = this.articles.GetById(id);

            articleToUpdate.CategoryId = article.CategoryId;
            articleToUpdate.Title = article.Title;
            articleToUpdate.Content = article.Content;

            this.articles.SaveChanges();
        }
    }
}
