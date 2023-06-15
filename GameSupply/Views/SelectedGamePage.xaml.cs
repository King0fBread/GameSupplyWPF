using System;
using System.Collections.Generic;
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
    /// Interaction logic for SelectedGamePage.xaml
    /// </summary>
    public partial class SelectedGamePage : Page
    {
        private Game _affectedGame = new Game();
        public SelectedGamePage(Game selectedGame)
        {
            InitializeComponent();
            _affectedGame = selectedGame;
            gameCanvas.DataContext = _affectedGame;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void redactSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckGameInfoValidity())
            {
                GameSupplyContext.GetContext().SaveChanges();

                MessageBox.Show("Успешно изменено");
                PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
                PageNavigationManager.MainFrame.RemoveBackEntry();
            }
        }
        private bool CheckGameInfoValidity()
        {
            if(titleTextBox.Text.Length < 5)
            {
                MessageBox.Show("Слишком короткое название!");
                return false;
            }
            if(descriptionTextBox.Text.Length < 20)
            {
                MessageBox.Show("Слишком короткое описание!");
                return false;
            }
            if(priceTextBox.Text.Length == 0 || !int.TryParse(priceTextBox.Text, out int result))
            {
                MessageBox.Show("Невозможная цена!");
                return false;
            }

            return true;
        }
    }
}
