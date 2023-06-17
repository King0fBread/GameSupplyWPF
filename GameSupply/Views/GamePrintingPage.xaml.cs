using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
    /// Interaction logic for GamePrintingPage.xaml
    /// </summary>
    public partial class GamePrintingPage : Page
    {
        public GamePrintingPage()
        {
            InitializeComponent();
            var gamesList = GameSupplyContext.GetContext().Games.ToList();

            foreach (var i in gamesList)
            {
                gamesComboBox.Items.Add(i.Title);
            }
            SetTextForPrinting();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void pdfPrintButton_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource documentSource = flowDocument;
                printDialog.PrintDocument(documentSource.DocumentPaginator, "Game Info");
            }
        }
        private void gamesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            SetTextForPrinting();
        }
        private void SetTextForPrinting()
        {
            var selectedGame = GameSupplyContext.GetContext().Games.First(p => p.Title == gamesComboBox.SelectedItem.ToString());
            gameInfoText.Text = selectedGame.Title + "\n" + selectedGame.Description + "\n" + "Цена ($): " + selectedGame.Price + "\n" + "Ссылка на скачивание: " + selectedGame.DownloadLink;
        }
    }
}
