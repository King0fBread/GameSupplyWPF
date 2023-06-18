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
        /// <summary>
        /// Аутентификация информации пользователя при попытке входа
        /// </summary>
        /// <param name="loginInput">Введенный пользователем логин</param>
        /// <param name="passwordInput">Введенный пользователем логин</param>
        /// <returns>Индекс проведенной операции, влияющий на дальнейшие дейтсвия</returns>
        private int AuthenticateUser(string loginInput, string passwordInput)
        {
            var context = GameSupplyContext.GetContext().Users.FirstOrDefault(p => p.Login == loginInput);
            if (context != null)
            {
                if(BCrypt.Net.BCrypt.EnhancedVerify(passwordInput, context.Password))
                {
                    UserInfo.User = context;
                    if(context.Status == "Admin") {
                        StatusContainer.UserStatus = 2;
                    }
                    else
                    {
                        StatusContainer.UserStatus = 1;
                    }
                    return 0;
                }
                return 1;
            }
            return 2;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            int authResult = AuthenticateUser(UsernameTextBox.Text, PasswordBox.Password);
            switch (authResult)
            {
                case 0:
                    PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
                    PageNavigationManager.MainFrame.RemoveBackEntry();
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

        private void AnnonymousLoginButton_Click(object sender, RoutedEventArgs e)
        {
            StatusContainer.UserStatus = 0;
            UserInfo.User = new User();
            PageNavigationManager.MainFrame.Navigate(new GamesCatalogue());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void newAccountButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new NewUserPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }
    }
}
