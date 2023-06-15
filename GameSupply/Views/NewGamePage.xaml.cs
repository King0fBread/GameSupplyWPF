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
            if (CheckGameInfoValidity())
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
        private bool CheckGameInfoValidity()
        {
            if (titleTextBox.Text.Length < 5)
            {
                MessageBox.Show("Слишком короткое название!");
                return false;
            }
            if (descriptionTextBox.Text.Length < 20)
            {
                MessageBox.Show("Слишком короткое описание!");
                return false;
            }
            if (priceTextBox.Text.Length == 0 || !int.TryParse(priceTextBox.Text, out int result))
            {
                MessageBox.Show("Невозможная цена!");
                return false;
            }
            if (!linkTextBox.Text.Contains("https://"))
            {
                MessageBox.Show("Невозможная ссылка!");
                return false;
            }
            return true;
        }
    }
}
