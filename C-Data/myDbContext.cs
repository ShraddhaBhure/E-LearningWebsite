using C_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace C_Data
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options)
           : base(options)
        {

        }
        public DbSet<Login> Login { get; set; }

        public DbSet<RegisterCourseData> RegisterData { get; set; }
        public DbSet<Contactus> Contactus { get; set; }
        public DbSet<OnlineClass> OnlineClass { get; set; }

        public DbSet<BooksLibrary> BooksLibrary { get; set; }
        public DbSet<HomeProjects> HomeProjects { get; set; }
    }
}
