using Blog_DAL.Models;
using Blog_DAL.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
