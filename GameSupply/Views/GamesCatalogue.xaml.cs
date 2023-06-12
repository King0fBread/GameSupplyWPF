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

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for GamesCatalogue.xaml
    /// </summary>
    public partial class GamesCatalogue : Page
    {
        public GamesCatalogue()
        {
            InitializeComponent();
            var availableGames = GameSupplyContext.GetContext().Games.ToList();
            GamesListBoxData.ItemsSource = availableGames;
        }
    }
}
