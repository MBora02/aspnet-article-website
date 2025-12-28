using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleWebsite.Persistence.Context
{
    public class ArticleWebsiteContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=**********\\SQLEXPRESS;initial catalog=ArticleWebsiteDb;integrated security=true;TrustServerCertificate=true");
        }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleStatus> ArticleStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TagCloud> TagClouds { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Faq> Faqs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { AppRoleId = 1, AppRoleName = "Admin" },
                new AppRole { AppRoleId = 2, AppRoleName = "Instructor" },
                new AppRole { AppRoleId = 3, AppRoleName = "Student" }
            );

            modelBuilder.Entity<ArticleStatus>().HasData(
                new ArticleStatus { ArticleStatusId = 1, Name = "Saved" },
                new ArticleStatus { ArticleStatusId = 2, Name = "Pending" },
                new ArticleStatus { ArticleStatusId = 3, Name = "Approved" },
                new ArticleStatus { ArticleStatusId = 4, Name = "Rejected" }
            );
        }
    }
}
