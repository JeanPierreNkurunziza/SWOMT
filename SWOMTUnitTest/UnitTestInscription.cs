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
   public class UnitTestInscription
    {
        [TestMethod]
        public void CheckExactNbrParticipantPerModule() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            int TotalNombreParticipant = 5;
            int IdSiteModule = 5;
            int reponseExcepted = TotalNombreParticipant;
            // Act

            int ActualResponse = InscriptionService.GetNbreTotalParticipantParModule(IdSiteModule); 
            // Assert


            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected date of the module is the real one ");
        }
        [TestMethod]
        public void CheckNbrOfParticipantOnListEttente()
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            int TotalNombreParticipantlistAttentParticipant = 2;
            int IdSiteModule = 5;
            int reponseExcepted = TotalNombreParticipantlistAttentParticipant;
            // Act

            int ActualResponse = InscriptionService.GetListAttenteParticipantPerModule(IdSiteModule).Count(); 
            // Assert


            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the number of waiting list per module is the real one ");
        }
        [TestMethod]
        public void CheckNbrOfParticipantOnFinalList() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            int TotalNombreParticipantlistAttentParticipant = 3;
            int IdSiteModule = 5;
            int reponseExcepted = TotalNombreParticipantlistAttentParticipant;
            // Act

            int ActualResponse = InscriptionService.GetListParticipantPerModule(IdSiteModule).Count(); 
            // Assert


            Assert.AreEqual(reponseExcepted, ActualResponse, "Test if the expected final list number of the participant per module is the real one ");
        }
    }
}
