namespace NewsSystem.Data.Services.Contracts
{
    using Models;
    using System.Linq;

    public interface ICategoriesServices
    {
        IQueryable<Category> GetAll();
        Category GetById(int id);
        void DeleteById(int id);
        void UpdateNameById(int id, string name);
        void Create(string name);

    }
}
