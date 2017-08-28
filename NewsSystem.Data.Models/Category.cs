namespace NewsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }
        public int Id { get; set; }
        [Required]
        [Index(IsUnique = true)]

        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

    }
}
