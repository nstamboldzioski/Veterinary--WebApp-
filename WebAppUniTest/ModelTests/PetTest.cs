using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp123.Models;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace WebApp123.Models
{
    [TestClass]
    public class VaccinesModelTest
    {
        [TestMethod]
        public void VaccineProperties_AreSetCorrectly()
        {
            // Arrange
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Luna", Age = 5 },
                new Pet { Id = 2, Name = "Ava", Age = 3 }
            };

            // Act
            var vaccine = new Vaccine
            {
                Id = 3,
                Name = "Rabies Vaccine",
                Pets = pets
            };

            // Assert
            Assert.AreEqual(3, vaccine.Id);
            Assert.AreEqual("Rabies Vaccine", vaccine.Name);
            CollectionAssert.AreEqual(pets, vaccine.Pets);
        }
    }
}

  
    
     
        
            
        
    
