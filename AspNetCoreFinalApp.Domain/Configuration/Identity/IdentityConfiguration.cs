using AspNetCoreFinalApp.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain.Configuration.Identity
{
    public class IdentityConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(keys => new { keys.RoleId, keys.UserId });

            builder.HasOne(virtualElementFromAppUserRole => virtualElementFromAppUserRole.User)
                .WithMany(virtualCollectionInAppUser => virtualCollectionInAppUser.UserRoles)
                .HasForeignKey(intParamForForeignKeySettings => intParamForForeignKeySettings.UserId)
                .IsRequired();

            builder.HasOne(virtualElementFromAppUserRole => virtualElementFromAppUserRole.Role)
                .WithMany(virtualCollectionInAppRole => virtualCollectionInAppRole.UserRoles)
                .HasForeignKey(intParamForForeignKeySettings => intParamForForeignKeySettings.RoleId)
                .IsRequired();
        }
    }
}
