using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWOMT.Views;
using MyApps.Domain.Service;
using MyApps.Application;
using MyApps.Infrastructure.DB;

namespace SWOMTUnitTest
{
    [TestClass]
   public class UnitTestPresence
    {
        [TestMethod]
        public void CheckDateDebutValidModule()
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            DateTime dateDebut = new DateTime(2020, 09, 29);
            int IdSiteModule = 5;
            DateTime reponseExcepted = dateDebut; 
            // Act

            DateTime? ActualResponse = PresenceService.GetDateDebut(IdSiteModule); 
            // Assert


            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected date of the module is the real one ");
        }
        [TestMethod]
        public void InvalidCheckDateDebutValidModule() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            DateTime dateDebut = new DateTime(2020, 09, 29);
            int IdSiteModule = 1;
            DateTime reponseExcepted = dateDebut;
            // Act

            DateTime? ActualResponse = PresenceService.GetDateDebut(IdSiteModule);
            // Assert


            Assert.AreNotEqual(reponseExcepted, ActualResponse, "Test if the expected date is not equal with the real date of the module ");
        }
        [TestMethod]
        public void InvalidCheckDateFinValidModule()
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            DateTime dateFin = new DateTime(2020, 12, 31);
            int IdSiteModule = 5;
            DateTime reponseExcepted = dateFin;
            // Act

            DateTime? ActualResponse = PresenceService.GetDateFin(IdSiteModule);
            // Assert


            Assert.AreNotEqual(reponseExcepted, ActualResponse, "Test if the expected date is not equal with the real date of the module ");
        }
        public void CheckDateFinValidModule()
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            DateTime dateFin = new DateTime(2020, 12, 31);
            int IdSiteModule = 15;
            DateTime reponseExcepted = dateFin;
            // Act

            DateTime? ActualResponse = PresenceService.GetDateFin(IdSiteModule);
            // Assert


            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected date is not equal with the real date of the module ");
        }

        [TestMethod]
        public void CheckTheNameOfModule()
        {

            // Arrange

            int IdSiteModule = 5;

            string reponseExcepted = "Projet appraisal";
            // Act

            string ActualResponse = ResultatService.GetNomModule(IdSiteModule); 
            // Assert

            // double actual = account.Balance;
            Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom module planifié est correcte ");
        }


    }
}
