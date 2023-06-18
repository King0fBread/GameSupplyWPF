using System;
using System.Collections.Generic;
using BCrypt.Net;

#nullable disable

namespace GameSupply.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Game> Games { get; set; } = new List<Game>();

        public User()
        {

        }
        public User(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
            Status = "Publisher";
        }
    }
}
