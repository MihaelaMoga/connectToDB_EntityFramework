using connectToBD_EntityFramework.PageModels.POM;
using connectToBD_EntityFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.POM
{
   class MainPage : BasePage
    {
        //selector
        const string checkMainPageSelector = "#homepage > div.title.hidden-xs.hidden-sm > h1 > strong"; //css
        const string cookiesSelector = "#cookies-consent > div > div > div:nth-child(2) > div > div.accept-cookies.col-xs-offset-4.col-xs-4.col-sm-offset-0.col-sm-3.pull-right.col-lg-2.text-right > button > span";



        //constructorul mosteneste constructorul din BasePage => avem nevoie de constructor pt a crea ulterior obiecte de tip MainPage in clasa MainPageTests.cs
        public MainPage(IWebDriver driver) : base(driver)
        {
        }



        //verific ca sunt pe pagina principala
        public string CheckMainPage()
        {
            return Utils.WaitForExplicitElement(driver,10,By.CssSelector(checkMainPageSelector)).Text;
        }

        public void AcceptCookies()
        {
            Utils.WaitForElementClickable(driver, 10, By.CssSelector(cookiesSelector)).Click();
        }

    }
}
