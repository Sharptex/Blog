using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Guid> Tags { get; set; }
    }
}
