using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Author_id).IsRequired();
            builder.HasOne(p => p.Author).WithMany(p => p.Posts).HasForeignKey(p => p.Author_id).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.Tags).WithMany(p => p.Posts).UsingEntity(j => j.ToTable("post_tag"));
        }
    }
}
