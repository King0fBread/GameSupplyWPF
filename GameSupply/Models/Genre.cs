using System;
using System.Collections.Generic;

#nullable disable

namespace GameSupply.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public int IdGenre { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
