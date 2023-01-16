using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using KutayBlogProjesi.Models.Entities.Abstract;

namespace KutayBlogProjesi.Models.Entities.Concrete
{
    public class Article : BaseEntity
    {
      

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ArticlePicture { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        //Navigation Properties
        public virtual User Author { get; set; }
    }
}
