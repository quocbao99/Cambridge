using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Utilities;
using static Utilities.CatalogueEnums;

namespace Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasData(
                    new Users()
                    {
                        Id = new Guid("0DEBFF1D-AC80-4E2D-BE24-3151B26F2176"),
                        Username = "admin",
                        FullName = "Mona Media",
                        Phone = "1900 636 666",
                        Email = "admin@mona-media.com",
                        Address = "373/226 Lý Thường Kiệt, P8, Q. Tân Bình, HCM",
                        Status = ((int)userStatus.active),
                        Birthday = 0,
                        Password = SecurityUtilities.HashSHA1("23312331"),
                        Gender = 0,
                        Created = Timestamp.Now(),
                        IsAdmin = true,
                        CreatedBy = Guid.Empty,
                        Deleted = false,
                        Active = true
                    }
                );
        }
    }
}
