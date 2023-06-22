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
            if (CheckGameInfoValidity(titleTextBox.Text, descriptionTextBox.Text, priceTextBox.Text))
            {
                GameSupplyContext.GetContext().SaveChanges();

                MessageBox.Show("Успешно изменено");
                PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
                PageNavigationManager.MainFrame.RemoveBackEntry();
            }
        }
        /// <summary>
        /// Проверяет поля при редактировании игры на полноту и корректность данных
        /// </summary>
        /// <returns>Булевый результат проверки</returns>
        public static bool CheckGameInfoValidity(string title, string description, string price)
        {
            if(title.Length < 5)
            {
                MessageBox.Show("Слишком короткое название!");
                return false;
            }
            if(description.Length < 20)
            {
                MessageBox.Show("Слишком короткое описание!");
                return false;
            }
            if(price.Length == 0 || !int.TryParse(price, out int result))
            {
                MessageBox.Show("Невозможная цена!");
                return false;
            }

            return true;
        }
    }
}
