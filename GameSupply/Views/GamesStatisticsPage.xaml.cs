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
using LiveCharts.Defaults;

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for GamesStatisticsPage.xaml
    /// </summary>
    public partial class GamesStatisticsPage : Page
    {
        public SeriesCollection Series = new SeriesCollection();
        public GamesStatisticsPage()
        {
            InitializeComponent();

            PopulateGamesGraph();
        }
        /// <summary>
        /// Заполняет граф игр релевантной информацией об их жанрах
        /// </summary>
        private void PopulateGamesGraph()
        {
            var genresList = GameSupplyContext.GetContext().Genres.ToList();
            foreach (var i in genresList)
            {
                Series.Add(new PieSeries
                {
                    Title = i.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(i.Games.Count) },
                });
            }
            genresPieChart.Series = Series;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }
    }
}
