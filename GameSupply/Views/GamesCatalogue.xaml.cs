using System;
using System.Diagnostics;
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
        private List<Game> _availableGames = new List<Game>();
        private bool _searchButtonFilterActive = false;
        public GamesCatalogue()
        {
            InitializeComponent();
            _availableGames = GameSupplyContext.GetContext().Games.ToList();
            GamesListBoxData.ItemsSource = _availableGames;

            if(UserInfo.User != null && StatusContainer.UserStatus != 0)
            {
                accountNameTextBlock.Text = "Вы зарегистрированы как: " + UserInfo.User.Login;

                addGameButton.Visibility = Visibility.Visible;
                myGamesButton.Visibility = Visibility.Visible;
                statisticsButton.Visibility = Visibility.Visible;
                loginHistoryButton.Visibility = Visibility.Visible;
            }
            else
            {
                accountNameTextBlock.Text = "Просмотр в анонимном режиме";

                addGameButton.Visibility = Visibility.Hidden;
                myGamesButton.Visibility = Visibility.Hidden;
                statisticsButton.Visibility = Visibility.Hidden;
                loginHistoryButton.Visibility = Visibility.Hidden;
            }

            AddCurrentUserToHistoryList();
        }
        private void AddCurrentUserToHistoryList()
        {
            GameSupplyContext db = new GameSupplyContext();
            if(StatusContainer.UserStatus != 0)
            {
                LoginSessionsHistory newSession = new LoginSessionsHistory(UserInfo.User.Status, UserInfo.User.IdUser);
                db.Add(newSession);
                db.SaveChanges();
            }
        }

        private void genreComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var allGames = _availableGames;
            if (genreComboBox.SelectedIndex == 0)
            {
                GamesListBoxData.ItemsSource = allGames;
                return;
            }
            GamesListBoxData.ItemsSource = allGames.Where(p => p.IdGenre == genreComboBox.SelectedIndex);
        }
        private void buttonTitleSearch_Click(object sender, RoutedEventArgs e)
        {
            var allGames = _availableGames;
            if (!_searchButtonFilterActive)
            {
                GamesListBoxData.ItemsSource = allGames.Where(p => p.Title.Contains(textBoxTitle.Text));
                _searchButtonFilterActive = true;
            }
            else
            {
                GamesListBoxData.ItemsSource = _availableGames;
                _searchButtonFilterActive = false;
            }
        }
        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new AuthenticationPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void myGamesButton_Click(object sender, RoutedEventArgs e)
        {
            GamesListBoxData.ItemsSource = _availableGames.Where(p => p.IdPublisher == UserInfo.User.IdUser);
        }

        private void addGameButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new NewGamePage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesStatisticsPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void loginHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new LoginHistoryPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void redactGame_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedGame = button.DataContext as Game;
            PageNavigationManager.MainFrame.Navigate(new SelectedGamePage(selectedGame));
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void deleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameSupplyContext db = new GameSupplyContext();

            var button = sender as Button;
            var selectedGame = button.DataContext as Game;

            if(MessageBox.Show("Удалить игру?", "Подтвердите удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Remove(selectedGame);
                db.SaveChanges();

                _availableGames = GameSupplyContext.GetContext().Games.ToList();
                GamesListBoxData.ItemsSource = _availableGames;

                MessageBox.Show("Удалено!");
            }
        }

        private void gameItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var stackPanel = sender as StackPanel;
            var selectedGame = stackPanel.DataContext as Game;
            var selectedGamePublisher = GameSupplyContext.GetContext().Users.First(p => p.IdUser == selectedGame.IdPublisher);

            if (MessageBox.Show(selectedGame.Description + Environment.NewLine + "От поставщика: " + selectedGamePublisher.Login + Environment.NewLine + Environment.NewLine +
                "Скачать игру?", "Тестовое скачивание для разработчиков", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = selectedGame.DownloadLink,
                    UseShellExecute = true
                });
            }
        }
    }
}
