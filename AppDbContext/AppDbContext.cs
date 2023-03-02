using Entities;
using Entities.Catalogue;
using Entities.Configuration;
using Extensions;
using Interface.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            //modelBuilder.Entity<OTPHistories>(x => x.ToTable("OTPHistories"));
            //modelBuilder.Entity<SMSEmailTemplates>(x => x.ToTable("SMSEmailTemplates"));

            //#region Configuration
            //modelBuilder.Entity<EmailConfigurations>(x => x.ToTable("EmailConfigurations"));
            //modelBuilder.Entity<SMSConfigurations>(x => x.ToTable("SMSConfigurations"));
            //#endregion

            //Data seeding (tạo dữ liệu mẫu - ....Extension.ModelBuilderExtensions)
            //modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Districts> Districts { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<EmailConfigurations> EmailConfigurations { get; set; }
        public DbSet<HangfireManage> HangfireManage { get; set; }
        public DbSet<OTPHistories> OTPHistories { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<UserNotification> UserNotification { get; set; }

        #region paypal
        #endregion

        #region stripe

        #endregion
    }
}