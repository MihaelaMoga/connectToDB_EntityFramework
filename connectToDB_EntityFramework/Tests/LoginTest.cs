using connectToBD_EntityFramework.POM;
using connectToBD_EntityFramework.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.Tests
{
    class LoginTest : BaseTest
    {

        string url = FrameworkConstants.GetUrl();


//METODA 7 de luare a datelor de test din baza de date MySql cu INSTALARE Microsoft.Entity Framework (adica VARIANTA 2 de citire a datelor din DB cu ajutorul Visual Studio/ pachet Microsoft.EntityFramework)

        //  static string condetails;
        private static IEnumerable<TestCaseData> GetCredentialsDbEf()
        {

            //citesc datele de conectare (server, user, password, port,database) din json si le salvez in variabila connString
            Other.DataModels.DbConnString connString = Utils.JsonRead<Other.DataModels.DbConnString>("appsettings.json");
            //salvez datele de conectare la baza de date in conDetails
            String conDetails = connString.ConnectionStrings.DefaultConnection;


            //Map the DB table to EF model
            //pt asta cream un obiect de tipul clasei CredentialsDbContext
            using (var context = new Other.CredentialsDbContext(conDetails))
            {
                //cu obiectul context apelam credentialsMM definit in clasa CredentialsDbContext si salvam valoarea in variabila credentials 
                var credentials = context.credential;
                //pt fiecare linie din credentials
                foreach (var cred in credentials)
                {
                    //returnam ca date de test: Username si Password
                    //practic asa inlocuim SELECT-ul din metoda 6 vezi mai sus
                    yield return new TestCaseData(cred.username, cred.password);
                }
            }
        }



        [Category("Smoke")]
        [Test, TestCaseSource("GetCredentialsDbEf"), Order(2)]
        public void LoginCuDB_EntitityFramework(string username, string password)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

            _driver.Navigate().GoToUrl(url);

            MainPage mp = new MainPage(_driver);
            mp.AcceptCookies();


            CreareContPage goToCreareCont = new CreareContPage(_driver);
            goToCreareCont.GoToContNou();

            LoginPage lp1 = new LoginPage(_driver);
            Assert.AreEqual("Am deja un cont la Vexio", lp1.CheckLoginPage());
            lp1.Login(username, password);
            Assert.AreEqual("Informatii cont", lp1.CheckAfterLogin());



            //in testName vars numele testului (Name este numele testului daca exista)
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);

        }







    }
}
