using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameSupply.Models;
using GameSupply.StandaloneScripts;

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for GamesCatalogue.xaml
    /// </summary>
    public partial class GamesCatalogue : Page
    {
        private List<Game> availableGames = new List<Game>();
        private bool searchButtonFilterActive = false;
        public GamesCatalogue()
        {
            InitializeComponent();
            availableGames = GameSupplyContext.GetContext().Games.ToList();
            GamesListBoxData.ItemsSource = availableGames;

            if(UserInfo.User != null && StatusContainer.UserStatus != 0)
            {
                accountNameTextBlock.Text = "Вы зарегистрированы как: " + UserInfo.User.Login;
            }
            else
            {
                accountNameTextBlock.Text = "Просмотр в анонимном режиме";
            }
        }

        private void genreComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var allGames = availableGames;
            switch (genreComboBox.SelectedIndex)
            {
                case 0:
                    GamesListBoxData.ItemsSource = allGames;
                    break;
                case 1:
                    GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == 1);
                    break;
                case 2:
                    GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == 2);
                    break;
                case 3:
                    GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == 3);
                    break;
                case 4:
                    GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == 4);
                    break;
                case 5:
                    GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == 5);
                    break;
            }
        }
        private void buttonTitleSearch_Click(object sender, RoutedEventArgs e)
        {
            var allGames = availableGames;
            if (!searchButtonFilterActive)
            {
                GamesListBoxData.ItemsSource = allGames.Where(p => p.Title.Contains(textBoxTitle.Text));
                searchButtonFilterActive = true;
            }
            else
            {
                GamesListBoxData.ItemsSource = availableGames;
                searchButtonFilterActive = false;
            }
        }
        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new AuthenticationPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void myGamesButton_Click(object sender, RoutedEventArgs e)
        {
            GamesListBoxData.ItemsSource = availableGames.Where(p => p.IdPublisher == UserInfo.User.IdUser);
        }
    }
}
