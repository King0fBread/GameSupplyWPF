using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameSupply.Views;

namespace GameSupplyTests
{
    [TestFixture]
    class AuthorizationTests
    {
        [Test]
        public void AuthenticateUser_WithCorrectCredentials_ReturnsIndexZero()
        {
            //Arrange
            string login = "shelby0_0";
            string password = "wasdwasd";
            //Act
            int resultIndex = AuthenticationPage.AuthenticateUser(login, password);
            //Assert
            int expectedIndex = 0;
            Assert.AreEqual(resultIndex, expectedIndex);
        }
        [Test]
        public void AuthenticateUser_WithIncorrectLogin_ReturnsIndexTwo()
        {
            //Arrange
            string login = "fakeName";
            string password = "wasdwasd";
            //Act
            int resultIndex = AuthenticationPage.AuthenticateUser(login, password);
            //Assert
            int expectedIndex = 2;
            Assert.AreEqual(resultIndex, expectedIndex);
        }
        [Test]
        public void AuthenticateUser_WithIncorrectPassword_ReturnsIndexOne()
        {
            //Arrange
            string login = "shelby0_0";
            string password = "fakePassword";
            //Act
            int resultIndex = AuthenticationPage.AuthenticateUser(login, password);
            //Assert
            int expectedIndex = 1;
            Assert.AreEqual(resultIndex, expectedIndex);
        }
    }
}
