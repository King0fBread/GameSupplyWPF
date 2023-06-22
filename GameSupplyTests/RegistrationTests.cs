using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameSupply.Views;

namespace GameSupplyTests
{
    [TestFixture]
    class RegistrationTests
    {
        [Test]
        public void RegisterUser_WithCorrectCredentials_ReturnsTrue()
        {
            //Arrange
            string login = "newUser";
            string password = "newPassword";
            string email = "newUserEmail@gmail.com";
            //Act
            bool result = NewUserPage.GetFieldsValidity(login, password, password, email);
            //Assert
            bool expected = true;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void RegisterUser_WithIncorrectCredentials_ReturnsFalse()
        {
            //Arrange
            string login = "ddd";
            string password = "nerPassword";
            string email = "newUserEmail@gmail.com";
            //Act
            bool result = NewUserPage.GetFieldsValidity(login, password, password, email);
            //Assert
            bool expected = false;
            Assert.AreEqual(result, expected);
        }
    }
}
