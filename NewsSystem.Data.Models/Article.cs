namespace NewsSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    public class Article
    {
        public Article()
        {
            this.Likes = new HashSet<Like>();
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}