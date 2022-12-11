using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Name).IsRequired();
        }
    }
}
