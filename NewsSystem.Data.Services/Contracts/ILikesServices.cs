namespace NewsSystem.Data.Services.Contracts
{
    using Models;

    public interface ILikesServices
    {
        Like GetByAuthorIdAndArticleId(string userId, int articleId);
        void ChangeLike(string userId, int articleId);
        void Like(Like like);
    }
}
