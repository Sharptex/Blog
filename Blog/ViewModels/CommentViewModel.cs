using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Created_at { get; set; }
        public DateTimeOffset Updated_at { get; set; }
        public PostViewModel Post { get; set; }
        public UserViewModel Author { get; set; }
    }
}
