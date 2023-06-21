using C_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace C_Data
{
    public class myDbContext : DbContext
    {

        public myDbContext(DbContextOptions<myDbContext> options)
           : base(options)
        {

        }
      
        public Task<Login> FirstOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
        public DbSet<Login> Login { get; set; }

        public DbSet<RegisterCourseData> RegisterData { get; set; }
        public DbSet<Contactus> Contactus { get; set; }
        public DbSet<OnlineClass> OnlineClass { get; set; }

        public DbSet<BooksLibrary> BooksLibrary { get; set; }
        public DbSet<HomeProjects> HomeProjects { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleUpload> ArticleUpload { get; set; }
    }

}
