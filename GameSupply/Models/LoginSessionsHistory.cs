using System;
using System.Collections.Generic;

#nullable disable

namespace GameSupply.Models
{
    public partial class LoginSessionsHistory
    {
        public int? IdLoginSession { get; set; }
        public string UserStatus { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
