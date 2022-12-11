using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(b => b.Author_id).IsRequired();
            builder.Property(b => b.Post_id).IsRequired();
            builder.HasOne(p => p.Author).WithMany(p => p.Comments).HasForeignKey(p => p.Author_id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Post).WithMany(p => p.Comments).HasForeignKey(p => p.Post_id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
