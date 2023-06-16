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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.SeriesAlgorithms;
using GameSupply.StandaloneScripts;

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for GamesStatisticsPage.xaml
    /// </summary>
    public partial class GamesStatisticsPage : Page
    {
        public ChartValues<int> amountOfGamesByGenre = new ChartValues<int>();
        public GamesStatisticsPage()
        {
            InitializeComponent();
            var genresList = GameSupplyContext.GetContext().Genres.ToList();
            foreach(var i in genresList)
            {
                amountOfGamesByGenre.Add(i.Games.Count);
            }

            //pieHorrorSeries.Values.Add(amountOfGamesByGenre[1]);
            //piePuzzleSeries.Values.Add(amountOfGamesByGenre[2]);
            //pieSurvivalSeries.Values.Add(amountOfGamesByGenre[3]);
            //pieArcadeSeries.Values.Add(amountOfGamesByGenre[4]);

            genresPieChart.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Платформер",
                    Values = amountOfGamesByGenre,
                },
                new PieSeries
                {
                    Title = "Хорррор",
                    Values = amountOfGamesByGenre,
                },
                new PieSeries
                {
                    Title = "Пазл",
                    Values = amountOfGamesByGenre,
                },
                new PieSeries
                {
                    Title = "Выживание",
                    Values = amountOfGamesByGenre,
                },
                new PieSeries
                {
                    Title = "Аркада",
                    Values = amountOfGamesByGenre,
                },

            };

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }
    }
}
