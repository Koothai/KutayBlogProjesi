using KutayBlogProjesi.Models.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace KutayBlogProjesi.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext( DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            
            //modelBuilder.Entity<User>().HasData(new User(1, "Kutay", "1234","kutay@kutay.com"));
            //modelBuilder.Entity<Article>().Property(x => x.CreateDate).HasDefaultValueSql("getutcdate()");
        }
    }
}
