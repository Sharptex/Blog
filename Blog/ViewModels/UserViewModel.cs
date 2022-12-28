using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль", Prompt = "Введите пароль")]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<CommentViewModel> Comments { get; set; }

    }
}
