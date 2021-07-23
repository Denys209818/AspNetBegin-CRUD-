using AspNetCoreFinalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreFinalApp.Domain.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<AppCar>
    {
        public void Configure(EntityTypeBuilder<AppCar> builder)
        {
            builder.ToTable("tblAppCars");

            builder.HasKey("Id");

            builder.Property("DateCreate").IsRequired();
            builder.Property("Developer").IsRequired().HasMaxLength(255);
            builder.Property("Model").IsRequired().HasMaxLength(255);
            builder.Property("Image").IsRequired().HasMaxLength(255);
            builder.Property("Price").IsRequired();
            builder.Property("Year").IsRequired();
        }
    }
}
