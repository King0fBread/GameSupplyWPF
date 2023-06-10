using System;
using System.Collections.Generic;

#nullable disable

namespace GameSupply.Models
{
    public partial class User
    {
        public User()
        {
            Games = new HashSet<Game>();
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
