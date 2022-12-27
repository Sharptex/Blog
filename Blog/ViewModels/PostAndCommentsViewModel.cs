using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class PostAndCommentsViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Display(Name = "Контент")]
        public string Content { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public UserViewModel Author { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}
