using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_DAL.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Created_at { get; set; }

        public DateTimeOffset Updated_at { get; set; }

        public Guid Post_id { get; set; }

        public Post Post { get; set; }

        public string Author_id { get; set; }

        public User Author { get; set; }
    }
}
