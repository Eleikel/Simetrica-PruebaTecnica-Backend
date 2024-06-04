using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Domain.Entities;

namespace TaskManager.Infrastructure.Persistence.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.UserName)
                .HasMaxLength(255);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.HasIndex(e => e.UserName)
                .IsUnique();

            builder.Property(e => e.Phone)
                .HasMaxLength(50);
        }
    }
}
