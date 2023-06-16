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
    /// Interaction logic for LoginHistoryPage.xaml
    /// </summary>
    public partial class LoginHistoryPage : Page
    {
        public LoginHistoryPage()
        {
            InitializeComponent();
            loginHistoryDataGrid.ItemsSource = GameSupplyContext.GetContext().LoginSessionsHistories.ToList();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }
    }
}
