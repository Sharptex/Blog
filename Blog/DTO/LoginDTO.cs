using System;

namespace Blog.DTO
{
    public class LoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}