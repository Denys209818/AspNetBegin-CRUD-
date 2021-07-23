using AspNetCoreFinalApp.Domain.Configuration;
using AspNetCoreFinalApp.Domain.Configuration.Identity;
using AspNetCoreFinalApp.Domain.Entities;
using AspNetCoreFinalApp.Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain
{
    public class EFDataContext : IdentityDbContext<AppUser,AppRole, long, IdentityUserClaim<long>, AppUserRole, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
    {

        public EFDataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppCar> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Identity
            builder.ApplyConfiguration(new IdentityConfiguration());
            #endregion

            #region Car
            builder.ApplyConfiguration(new CarConfiguration());
            #endregion
        }
    }
}
