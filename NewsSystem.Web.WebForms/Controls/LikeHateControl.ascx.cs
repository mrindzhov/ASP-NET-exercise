namespace NewsSystem.Web.WebForms.Controls
{
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Data.Services;
    using Data.Services.Contracts;
    using Microsoft.AspNet.Identity;
    using System;

    public partial class LikeHateControl : System.Web.UI.UserControl
    {
        //[Inject]
        //public ILikesServices LikesServices { get; set; }

        public ILikesServices LikesServices { get; set; } =
            new LikesService(new GenericRepository<Like>(new NewsSystemDbContext()));

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Page.User.Identity.GetUserId();
            int articledId = int.Parse(this.Request.QueryString["id"]);
            Like like = this.LikesServices.GetByAuthorIdAndArticleId(userId, articledId);

            if (like == null)
            {
                return;
            }

            (like.Value ? this.btnLike : this.btnHate).Visible = false;
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            this.HandleLikeEvent(true);
        }

        protected void btnHate_Click(object sender, EventArgs e)
        {
            this.HandleLikeEvent(false);
        }

        private void HandleLikeEvent(bool isLike)
        {
            string userId = Page.User.Identity.GetUserId();
            int articledId = int.Parse(this.Request.QueryString["id"]);
            Like like = this.LikesServices.GetByAuthorIdAndArticleId(userId, articledId);

            if (like != null)
            {
                this.LikesServices.ChangeLike(userId, articledId);
                (like.Value ? this.btnLike : this.btnHate).Visible = false;
                (!like.Value ? this.btnLike : this.btnHate).Visible = true;
            }
            else
            {
                Like createdLike = new Like()
                {
                    AuthorId = Page.User.Identity.GetUserId(),
                    ArticleId = int.Parse(this.Request.QueryString["id"]),
                    Value = isLike
                };
                this.LikesServices.Like(createdLike);

                (createdLike.Value ? this.btnLike : this.btnHate).Visible = false;
                (!createdLike.Value ? this.btnLike : this.btnHate).Visible = true;
            }

        }
    }
}