using System;

namespace API.DTO
{
    public class CommentDTO
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid Post_id { get; set; }

        public string Author_id { get; set; }
    }
}
