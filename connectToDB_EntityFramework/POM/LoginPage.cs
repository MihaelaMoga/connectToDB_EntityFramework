using connectToBD_EntityFramework.PageModels.POM;
using connectToBD_EntityFramework.POM;
using connectToBD_EntityFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.POM
{
    class LoginPage : BasePage
    {


        //selectori
        const string checkLoginSelector = "#login > formfield > legend";//css
        const string emailSelector = "email";//id
        const string passSelector = "password";//id
        const string submitSelector = "#login > formfield > div:nth-child(5) > div > button";//css

        const string checkAfterLoginSelector = "#info_form > fieldset:nth-child(2) > legend"; //css



        //constructor in baza caruia voi face obiecte de tip LoginPage in teste
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }



        public string CheckLoginPage()
        {
            return Utilities.Utils.WaitForExplicitElement(driver,10,By.CssSelector(checkLoginSelector)).Text;
        }




        public void Login(string email, string pass)
        {
            var emailEl = Utils.WaitForExplicitElement(driver, 10, By.Id(emailSelector));
            emailEl.Clear();
            emailEl.SendKeys(email);

            var passEl = Utils.WaitForExplicitElement(driver, 10, By.Id(passSelector));
            passEl.Clear();
            passEl.SendKeys(pass);

            var submitEl = Utils.WaitForExplicitElement(driver, 10, By.CssSelector(submitSelector));
            submitEl.Submit();
        }



        public string CheckAfterLogin()
        {
            return Utils.WaitForExplicitElement(driver, 10, By.CssSelector(checkAfterLoginSelector)).Text;
        }



    }
}
