using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset Created_at { get; set; }

        public DateTimeOffset Updated_at { get; set; }

        public string Content { get; set; }

        public string Author_id { get; set; }

        public User Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
