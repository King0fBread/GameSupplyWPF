using System;
using System.Collections.Generic;
using GameSupply.StandaloneScripts;

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
        public string DownloadLink { get; set; }

        public virtual Genre IdGenreNavigation { get; set; }
        public virtual User IdPublisherNavigation { get; set; }
        public Game()
        {

        }
        public Game(int idPublisher, int idGenre, string title, string description, int price, string previewImage, string downloadLink)
        {
            IdPublisher = idPublisher;
            IdGenre = idGenre;
            Title = title;
            Description = description;
            Price = price;
            PreviewImage = previewImage;
            DownloadLink = downloadLink;
        }
        public string RedactVisibility
        {
            get
            {
                if(IdPublisher == UserInfo.User.IdUser || StatusContainer.UserStatus == 2)
                {
                    return "Visible";
                }
                else
                {
                    return "Hidden";
                }
            }
        }
    }
}
