using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsSystem.Data.Models;

namespace NewsSystem.Data
{
    public class NewsSystemDbContext : IdentityDbContext<User>, INewsSystemDbContext
    {
        public NewsSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Like> Likes { get; set; }


        public static NewsSystemDbContext Create()
        {
            return new NewsSystemDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Article>()
            //    .HasRequired(p => p.Category)
            //    .WithMany(x => x.Articles)
            //    .WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
