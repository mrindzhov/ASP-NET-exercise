namespace NewsSystem.Data.Services
{
    using Contracts;
    using System.Linq;
    using Models;
    using Repositories;
    using System;
    using System.Data.Entity;

    public class CategoriesServices : ICategoriesServices
    {
        private IRepository<Category> categories;
        public CategoriesServices(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public void Create(string name)
        {
            this.categories.Add(new Category() { Name = name });
            this.categories.SaveChanges();
        }

        public void DeleteById(int id)
        {
            this.categories.Delete(id);
            this.categories.SaveChanges();
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All().Include(s => s.Articles);
        }

        public Category GetById(int id)
        {
            return this.categories.GetById(id);
        }

        public void UpdateNameById(int id, string name)
        {
            this.categories.GetById(id).Name = name;
            this.categories.SaveChanges();
        }
    }
}
