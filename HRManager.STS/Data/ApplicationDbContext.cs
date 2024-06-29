using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRManager.STS.Models;

namespace HRManager.STS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "151dba72-2400-43d6-9e33-cadbb71b865b",
                Email = "marko@gmail.com",
                NormalizedEmail = "MARKO@GMAIL.COM",
                UserName = "marko@gmail.com",
                NormalizedUserName = "MARKO@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAECi3ahkgYfuCpckglBbY8R8Ah52Jk/FAXgAg7QNkul4+VWx4eADyFQ0FyS4cS8tFcg==",
                Role = "Admin"
            });

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "510df9ac-baca-43b6-9e4a-cdda5f419428",
                Email = "ana@gmail.com",
                NormalizedEmail = "ANA@GMAIL.COM",
                UserName = "ana@gmail.com",
                NormalizedUserName = "ANA@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEM90xgFIknZXv+QhkRbq/cmGkk5wwost6ScPVEPmrkQa8PrcqfOZHSzX+9aaGCQwSg==",
                Role = "Edit"
            });

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "91dd93b1-403c-4913-b7fe-917bb0c35996",
                Email = "filip@gmail.com",
                NormalizedEmail = "FILIP@GMAIL.COM",
                UserName = "filip@gmail.com",
                NormalizedUserName = "FILIP@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEAo6Wp95tWLS9RAPHrv0CmqkYk3Nio/Dxl0hjyx+i1CRA2jByJ6uQbyrW8e1wFVcfA==",
                Role = "View"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}