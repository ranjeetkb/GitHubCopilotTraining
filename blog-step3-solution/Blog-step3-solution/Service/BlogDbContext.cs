using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    /*
       This class should be used as DbContext to speak to database and should make the use of 
       Code first approach. 
       It should autogenerate the database based upon the model class in the application
   */
    public class BlogDbContext:DbContext
    {
        public BlogDbContext() { }
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : 
            base(options)
        {
            Database.EnsureCreated();
        }

        //DbSet for BlogPost
        public DbSet<BlogPost> BlogPosts { get; set; }=null!;     
      
        //DbSet for Categories
        public DbSet<Category> Categories { get; set; }=null!;
       
        //DbSet for User
        public DbSet<User> Users { get; set; }=null!;
    }
}
