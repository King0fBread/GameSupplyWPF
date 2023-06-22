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
using GameSupply.StandaloneScripts;
using GameSupply.Models;

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for NewGamePage.xaml
    /// </summary>
    public partial class NewGamePage : Page
    {
        public NewGamePage()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void addGame_Click(object sender, RoutedEventArgs e)
        {
            if (CheckGameInfoValidity(titleTextBox.Text, descriptionTextBox.Text, priceTextBox.Text, linkTextBox.Text))
            {
                GameSupplyContext db = new GameSupplyContext();
                Game newGame = new Game(UserInfo.User.IdUser, genreComboBox.SelectedIndex, titleTextBox.Text, descriptionTextBox.Text, int.Parse(priceTextBox.Text), imageSelectionTextBox.Text, linkTextBox.Text);
                db.Add(newGame);
                db.SaveChanges();

                MessageBox.Show("Успешно добавлено!");
                PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
                PageNavigationManager.MainFrame.RemoveBackEntry();
            }
        }
        /// <summary>
        /// Проверяет поля при публикации новой игры на полноту и корректность данных
        /// </summary>
        /// <returns>Булевый результат проверки</returns>
        public static bool CheckGameInfoValidity(string title, string description, string price, string link)
        {
            if (title.Length < 5)
            {
                MessageBox.Show("Слишком короткое название!");
                return false;
            }
            if (description.Length < 20)
            {
                MessageBox.Show("Слишком короткое описание!");
                return false;
            }
            if (price.Length == 0 || !int.TryParse(price, out int result))
            {
                MessageBox.Show("Невозможная цена!");
                return false;
            }
            if (!link.Contains("https://"))
            {
                MessageBox.Show("Невозможная ссылка!");
                return false;
            }
            return true;
        }
    }
}
