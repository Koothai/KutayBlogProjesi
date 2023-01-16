using KutayBlogProjesi.Models.Entities.Abstract;
using System.Collections.Generic;

namespace KutayBlogProjesi.Models.Entities.Concrete
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
