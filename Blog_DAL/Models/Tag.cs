using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
