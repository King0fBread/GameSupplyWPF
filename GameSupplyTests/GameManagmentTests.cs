using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameSupply.Views;

namespace GameSupplyTests
{
    [TestFixture]
    class GameManagmentTests
    {
        [Test]
        public void EditGame_WithCorrectInformation_ReturnsTrue()
        {
            //Arrange
            string title = "NewTitle";
            string description = "NewDescription texttexttexttexttexttexttexttexttexttexttexttext";
            string price = "3";
            //Act
            bool result = SelectedGamePage.CheckGameInfoValidity(title, description, price);
            //Assert
            bool expected = true;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void EditGame_WithIncorrectInformation_ReturnsFalse()
        {
            //Arrange
            string title = "NewTitle";
            string description = "NewDescription texttexttexttexttexttexttexttexttexttexttexttext";
            string price = "ddd";
            //Act
            bool result = SelectedGamePage.CheckGameInfoValidity(title, description, price);
            //Assert
            bool expected = false;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AddGame_WithCorrectInformation_ReturnsTrue()
        {
            //Arrange
            string title = "WarlocksPVP";
            string description = "NewDescription texttexttexttexttexttexttexttexttexttexttexttext";
            string price = "1";
            string link = "https://matix.li/8a2c8d5a4242";
            //Act
            bool result = NewGamePage.CheckGameInfoValidity(title, description, price, link);
            //Assert
            bool expected = true;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AddGame_WithIncorrectInformation_ReturnsFalse()
        {
            //Arrange
            string title = "WarlocksPVP";
            string description = "NewDescription texttexttexttexttexttexttexttexttexttexttexttext";
            string price = "ddd";
            string link = "https://matix.li/8a2c8d5a4242";
            //Act
            bool result = NewGamePage.CheckGameInfoValidity(title, description, price, link);
            //Assert
            bool expected = false;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void DeleteGame_WithRegiseredAccountPermissions_ReturnsTrue()
        {
            //Arrange
            int userPermissionsStatus = 1;
            //Act
            bool result = GamesCatalogue.GetUserStatus(userPermissionsStatus);
            //Assert
            bool expected = true;
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void DeleteGame_WithAbsentAccountPermissions_ReturnsFalse()
        {
            //Arrange
            int userPermissionsStatus = 0;
            //Act
            bool result = GamesCatalogue.GetUserStatus(userPermissionsStatus);
            //Assert
            bool expected = false;
            Assert.AreEqual(result, expected);
        }
    }
}
