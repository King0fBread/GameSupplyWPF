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

        private bool AuthenticateUser(string loginInput, string passwordInput)
        {
            //Login check
            var context = GameSupplyContext.GetContext().Users.FirstOrDefault(p => p.Login == loginInput);
            if (context != null)
            {
                //Password check
                if(BCrypt.Net.BCrypt.EnhancedVerify(passwordInput, context.Password))
                {
                    return true;
                }
                //Incorrect password
                return false;
            }
            //incorrect login
            return false;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(AuthenticateUser(UsernameTextBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("kruto");
            }
            else
            {
                MessageBox.Show("nee");
            }
        }
    }
}
