using System;
using WebApp123.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp123.ModelsTest
{
    [TestClass]
    public class PersonModelTest
    {
        [TestMethod]
        public void GetFullName_ReturnsFullName()
        {
            // Arrange
            var person = new Person
            {
                Name = "Naum",
                Surname = "Stamboldjioski",
                Age = 24
            };

            // Act
            var fullName = person.GetFullName();

            // Assert
            Assert.AreEqual("Naum Stamboldjioski", fullName);
        }
    }
}