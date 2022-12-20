using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Никнейм")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string PasswordReg { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
        public ICollection<Guid> Roles { get; set; }
    }
}
