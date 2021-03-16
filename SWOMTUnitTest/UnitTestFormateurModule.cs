using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWOMT.Views;
using MyApps.Domain.Service;
using MyApps.Application;
using MyApps.Infrastructure.DB;
using System.Linq;

namespace SWOMTUnitTest
{
    [TestClass]
   public class UnitTestFormateurModule
    {
        [TestMethod] 
        public void CheckExactNbrModulePerFormateur() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            int NbreModule = 2;
            int IdFormateur = 5;
            int reponseExcepted = NbreModule; 
            // Act

            int ActualResponse = FormateurModuleService.GetListModulesPerFormateur(IdFormateur).Count(); 
            // Assert

            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected number of the module is the real one ");
        }
        [TestMethod]
        public void CheckExactNbrFormateurPerModule() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            int NbreFormateur = 1;
            int IdModule = 5;
            int reponseExcepted = NbreFormateur;
            // Act

            int ActualResponse = FormateurModuleService.GetListFormateurPerModules(IdModule).Count();  
            // Assert

            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected number of the formateur is the real one "); 
        }
    }
}
