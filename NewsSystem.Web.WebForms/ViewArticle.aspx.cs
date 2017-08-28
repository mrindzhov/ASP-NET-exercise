namespace NewsSystem.Web.WebForms
{
    using Data.Services.Contracts;
    using Ninject;
    using System;
    using System.Web.ModelBinding;
    public partial class ViewArticle : System.Web.UI.Page
    {
        [Inject]
        public IArticlesServices ArticlesService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public NewsSystem.Data.Models.Article fvDetails_GetItem([QueryString]string id)
        {
            int idInt;
            bool isInteger = int.TryParse(id, out idInt);
            if (isInteger)
            {
                return this.ArticlesService.GetById(idInt);
            }
            else
            {
                return null;
            }
        }
    }
}