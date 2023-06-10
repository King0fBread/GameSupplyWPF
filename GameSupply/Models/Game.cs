﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GameSupply.Models
{
    public partial class Game
    {
        public int IdGame { get; set; }
        public int IdPublisher { get; set; }
        public int IdGenre { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PreviewImage { get; set; }

        public virtual Genre IdGenreNavigation { get; set; }
        public virtual User IdPublisherNavigation { get; set; }
    }
}