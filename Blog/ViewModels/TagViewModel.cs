using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public ICollection<PostViewModel> Posts { get; set; }
    }
}
