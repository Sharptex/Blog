using System;
using System.Collections.Generic;

namespace API.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<Guid> Roles { get; set; }
    }
}
