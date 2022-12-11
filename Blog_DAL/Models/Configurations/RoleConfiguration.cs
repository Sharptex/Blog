using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Name).IsRequired();
            builder.HasMany(p => p.Users).WithMany(p => p.Roles).UsingEntity(j => j.ToTable("role_user"));
        }
    }
}
