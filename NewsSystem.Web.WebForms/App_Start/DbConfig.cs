namespace NewsSystem.Web.WebForms
{
    using Data;
    using Data.Migrations;
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