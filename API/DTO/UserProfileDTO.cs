using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<Guid> Roles { get; set; }
    }
}
