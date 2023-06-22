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
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        public NewUserPage()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigationManager.MainFrame.Navigate(new AuthenticationPage());
            PageNavigationManager.MainFrame.RemoveBackEntry();
        }

        private void userRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (GetFieldsValidity(loginTextBox.Text, passwordTextBox.Text, secondPasswordTextBox.Text, emailTextBox.Text))
            {
                GameSupplyContext db = new GameSupplyContext();
                User newUser = new User(loginTextBox.Text, BCrypt.Net.BCrypt.EnhancedHashPassword(passwordTextBox.Text), emailTextBox.Text);

                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Аккаунт успешно добавлен!");

                PageNavigationManager.MainFrame.Navigate(new AuthenticationPage());
                PageNavigationManager.MainFrame.RemoveBackEntry();
            }
        }
        /// <summary>
        /// Проверяет поля при публикации новой игры на полноту и корректность данных
        /// </summary>
        /// <returns>Булевый результат проверки</returns>
        public static bool GetFieldsValidity(string login, string password, string passwordRepeatField, string email)
        {
            if(login.Length < 5)
            {
                MessageBox.Show("Слишком короткий логин!");
                return false;
            }
            if(password != passwordRepeatField)
            {
                MessageBox.Show("Пароль повторен ошибочно!");
                return false;
            }
            if(password.Length < 6)
            {
                MessageBox.Show("Слишком короткий пароль!");
                return false;
            }
            if (!email.Contains("@gmail.com"))
            {
                MessageBox.Show("Невозможный адрес почты!");
                return false;
            }
            if(email.Length < 15)
            {
                MessageBox.Show("Слишком короткий адрес почты!");
                return false;
            }

            return true;
        }
    }
}
