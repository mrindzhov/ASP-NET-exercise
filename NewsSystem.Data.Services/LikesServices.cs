namespace NewsSystem.Data.Services
{
    using Models;
    using Contracts;
    using Repositories;
    using System.Linq;
    using System;

    public class LikesService : ILikesServices
    {
        private IRepository<Like> likes;
        public LikesService(IRepository<Like> likes)
        {
            this.likes = likes;
        }
        public Like GetByAuthorIdAndArticleId(string userId, int articleId)
        {
            return this.likes.All().FirstOrDefault(l => l.AuthorId == userId && l.ArticleId == articleId);
        }

        public void ChangeLike(string userId, int articleId)
        {
            Like like = this.GetByAuthorIdAndArticleId(userId, articleId);
            like.Value = !like.Value;
            this.likes.SaveChanges();
        }

        public void Like(Like like)
        {
            this.likes.Add(like);
            this.likes.SaveChanges();
        }
    }
}
