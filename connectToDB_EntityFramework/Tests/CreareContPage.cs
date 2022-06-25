using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using connectToBD_EntityFramework.PageModels.POM;
using connectToBD_EntityFramework.Utilities;

namespace connectToBD_EntityFramework.POM
{
    class CreareContPage : BasePage
    {
        //selectori
        const string contClientSelector = "hidden-md";//class
        const string divContClientSelector = "nav-stacked"; //class
        const string creazaContSelector = "div.popover-content > ul > li:nth-child(2) > a > span"; //css
        const string checkContNouSelector = "//*[@id='register']/formfield/legend"; //xpath

       

        const string prenumeSelector = "newfirstname";//id
        const string numeSelector = "newlastname";//id
        const string phoneSelector = "telephone";//id
        const string mailSelector = "newemail";//id
        const string passSelector = "newpassword";//id
        const string repeatPassSelector = "newpasswordretype";//id
        const string newsletterSelector = "newsletter";//id
        const string termsSelector = "agree";//id
       // const string submitSelector = "btn-primary";//class
        const string submitSelector = "#register > formfield > div:nth-child(9) > div > button";//css


        const string messagePopupSelector = "body > div > div > button";//css
        const string afterCreareContNouSelector = "//*[@id='info_form']/fieldset[1]/legend";//css



        //constructor in baza caruia voi crea obiecte in test
        public CreareContPage(IWebDriver driver) : base(driver)
        {
        }


     
        public void GoToContNou()
        {
            var contClientEl = Utils.WaitForElementClickable(driver,10,By.ClassName(contClientSelector));
            contClientEl.Click();

            var divContClient = Utils.WaitForExplicitElement(driver,10,By.ClassName(divContClientSelector));
            var creazaContNou = divContClient.FindElement(By.CssSelector(creazaContSelector));
            creazaContNou.Click();

        }


        public string CheckContNou()
        {
           var checkContNou = Utils.WaitForExplicitElement(driver,10,By.XPath(checkContNouSelector));
           return checkContNou.Text;
        }



       


        public void CreareContNou(string prenume,string nume,string phone,string mail,string pass,string repeatPass,bool newsletter, bool agree)
        {
            var prenumeEl = driver.FindElement(By.Id(prenumeSelector));
            prenumeEl.Clear();
            prenumeEl.SendKeys(prenume);

            var numeEl = driver.FindElement(By.Id(numeSelector));
            numeEl.Clear();
            numeEl.SendKeys(nume);

            var phoneEl = driver.FindElement(By.Id(phoneSelector));
            phoneEl.Clear();
            phoneEl.SendKeys(phone);

            var mailEl = driver.FindElement(By.Id(mailSelector));
            mailEl.Clear();
            mailEl.SendKeys(mail);


            var passEl = driver.FindElement(By.Id(passSelector));
            passEl.Clear();
            passEl.SendKeys(pass);

            var repeatPassEl = driver.FindElement(By.Id(repeatPassSelector));
            repeatPassEl.Clear();
            repeatPassEl.SendKeys(repeatPass);


            if(newsletter ==true)
            {
                var newsletterEl = driver.FindElement(By.Id(newsletterSelector));
                newsletterEl.Click();
            }

            if (agree == true)
            {
                var termsEl = driver.FindElement(By.Id(termsSelector));
                termsEl.Click();
            }


            

           IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
           js.ExecuteScript("window.scrollTo(950, 8)");
           var submitEl = Utils.WaitForExplicitElement(driver, 10, By.CssSelector(submitSelector));
           submitEl.Submit();

        }



        public string CkeckAfterCreareContNou()
        {
            
          return Utils.WaitForExplicitElement(driver,10,By.XPath(afterCreareContNouSelector)).Text;
        }


    }
}
