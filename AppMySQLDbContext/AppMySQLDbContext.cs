using Entities;
using Entities.Catalogue;
using Entities.Configuration;
using Interface.DbContext;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;

namespace AppMySQLDbContext
{
    //public class AppMySQLDbContext : DbContext, IAppDbContext
    //{
    //    public AppMySQLDbContext(DbContextOptions<AppMySQLDbContext> options) : base(options)
    //    {
    //    }
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //    }
    //    public DbSet<Districts> Districts { get; set; }
    //    public DbSet<Cities> Cities { get; set; }
    //    public DbSet<Users> Users { get; set; }
    //    public DbSet<Role> Role { get; set; }
    //    public DbSet<Menu> Menu { get; set; }
    //    public DbSet<Brand> Brand { get; set; }
    //    public DbSet<Order> Order { get; set; }
    //    public DbSet<EmailConfigurations> EmailConfigurations { get; set; }
    //    public DbSet<HangfireManage> HangfireManage { get; set; }
    //    public DbSet<OTPHistories> OTPHistories { get; set; }
    //    public DbSet<Notification> Notification { get; set; }
    //    public DbSet<UserNotification> UserNotification { get; set; }
    //}
}
