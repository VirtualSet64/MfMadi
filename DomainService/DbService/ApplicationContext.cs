using DomainService.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DbService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Advertising> Advertisings { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<News> Newses { get; set; } = null!;        
        public DbSet<MainMenu> MainMenus { get; set; } = null!;
        public DbSet<Partner> Partners { get; set; } = null!;
        public DbSet<Content> Contents { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();   // удаляем базу данных при первом обращении
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
