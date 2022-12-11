using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog_DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
