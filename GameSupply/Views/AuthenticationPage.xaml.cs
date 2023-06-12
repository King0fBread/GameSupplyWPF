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
using BCrypt.Net;
using GameSupply.Models;
using GameSupply.StandaloneScripts;
using System.Linq;

namespace GameSupply.Views
{
    /// <summary>
    /// Interaction logic for AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            InitializeComponent();
        }

        private int AuthenticateUser(string loginInput, string passwordInput)
        {
            //Login check
            var context = GameSupplyContext.GetContext().Users.FirstOrDefault(p => p.Login == loginInput);
            if (context != null)
            {
                //Password check
                if(BCrypt.Net.BCrypt.EnhancedVerify(passwordInput, context.Password))
                {
                    return 0;
                }
                //Incorrect password
                return 1;
            }
            //incorrect login
            return 2;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int authResult = AuthenticateUser(UsernameTextBox.Text, PasswordBox.Password);
            switch (authResult)
            {
                case 0:
                    PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
                    break;
                case 1:
                    MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordBox.Clear();
                    break;
                case 2:
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    UsernameTextBox.Clear();
                    PasswordBox.Clear();
                    break;
            }
        }
    }
}
