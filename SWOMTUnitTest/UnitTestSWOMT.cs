using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWOMT.Views;
using MyApps.Domain.Service;
using MyApps.Application;
using MyApps.Infrastructure.DB;

namespace SWOMTUnitTest
{
    [TestClass]
    public class UnitTestSWOMT
    {
        [TestMethod]
        public void CheckUserLoginNameValid() 
        {
           
            // Arrange
            //var utilisateur = UserService.GetAll();
            string UserName = "christine";
            string MotDePasse = "1010";
            string reponseExcepted = UserName;
            // Act
          
          string ActualResponse=  UserService.LoginUserNom(UserName, MotDePasse);
            // Assert
          
           // double actual = account.Balance;
            Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom d'utilisateur correspond à son mot de passe "); 
        }
        [TestMethod]
        public void CheckUserLoginNameInValid() 
        {

            // Arrange
            //var utilisateur = UserService.GetAll();
            string UserName = "Christinejjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj |||&&&&&&&######### /////////////// **********------";
            string MotDePasse = "dada";
            string reponseExcepted = null;
            // Act

            string ActualResponse = UserService.LoginUserNom(UserName, MotDePasse);
            // Assert

            // double actual = account.Balance;
            Assert.AreEqual(reponseExcepted, ActualResponse, true, "Le nom d'utilisateur ne correspond pas à son mot de passe ");
        }
        [TestMethod]
        public void CheckExamPlannedMatchWithTheNameOfModule() 
        {

            // Arrange
           
            int IdExamen =5;
           
            string reponseExcepted = "Projet appraisal";
            // Act

            string ActualResponse = ResultatService.GetNomModule(IdExamen);
            // Assert

            // double actual = account.Balance;
            Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom module d'un examen planifié est correcte  ");
        }
        [TestMethod]
        public void CheckExamPlannedMatchWithInvalidDataTheNameOfModule()
        {

            // Arrange

            int IdExamen = 5874;

            string reponseExcepted = "Projet appraisal";
            // Act
            try
            {
                string ActualResponse = ResultatService.GetNomModule(IdExamen);
                Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom module d'un examen planifié est correcte  ");
            }
            catch (System.NullReferenceException e)
            {
                // Assert

                
                //Assert.ThrowsException<System.ArgumentNullException>(() => ResultatService.GetNomModule(IdExamen));
                StringAssert.Contains(ResultatService.MessageOfNullableIdParameterException ,e.Message); 
                return; 
            }
            Assert.Fail("The expected exception was not thrown.");


        }
        [TestMethod]
        public void CheckIdInscriptionMatchWithNomParticipant()
        {

            // Arrange

            int IdInscription = 28;

            string reponseExcepted = "Tupont Aimable";
            // Act

            string ActualResponse = ResultatService.GetNomParticipant(IdInscription);
            // Assert

            // double actual = account.Balance;
            Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom de participant correspont son ID d'inscription dans la table Resultat ");
        }
        [TestMethod]
        public void CheckNullExceptionIfIdInscriptionIsNotRecognizedInResults()
        {

            // Arrange

            int IdInscription = 5874;

            string reponseExcepted = "Projet appraisal";
            // Act
            try
            {
                string ActualResponse = ResultatService.GetNomModule(IdInscription);
                Assert.AreEqual(reponseExcepted, ActualResponse, true, "Vérifier si le nom participant est correcte avec son id inscription  ");
            }
            catch (System.NullReferenceException e)
            {
                // Assert


                //Assert.ThrowsException<System.ArgumentNullException>(() => ResultatService.GetNomModule(IdExamen));
                StringAssert.Contains(ResultatService.MessageOfNullableIdParameterException, e.Message);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");


        }

    }
}
