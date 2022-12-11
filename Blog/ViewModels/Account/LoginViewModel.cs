using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Никнейм")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
