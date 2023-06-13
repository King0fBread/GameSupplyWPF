using System;
using System.Collections.Generic;
using System.Text;

namespace GameSupply.Models
{
    class UserInfo
    {
        private static User? currentUser { get; set; }
        public static User? User
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
    }
}
