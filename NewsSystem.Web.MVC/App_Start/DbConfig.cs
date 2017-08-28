namespace NewsSystem.Web.MVC.App_Start
{
    using NewsSystem.Data;
    using NewsSystem.Data.Migrations;
    using System.Data.Entity;

    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsSystemDbContext, Configuration>());
            NewsSystemDbContext.Create().Database.Initialize(true);
        }
    }
}