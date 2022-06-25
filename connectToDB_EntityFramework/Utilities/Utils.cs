using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace connectToBD_EntityFramework.Utilities
{
    class Utils
    {

//METODA 4 pt citire date de test dintr-un json
        //mapeaza fisierul jsonFile la clasa T
        public static T JsonRead<T>(string jsonFile)
        {
            string text = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<T>(text);
        }


        //click pe becul galben si selectez "using OpenQA.Selenium"
        public static IWebElement WaitForExplicitElement(IWebDriver driver, int seconds, By locator)
        {
            //definim wait-ul explicit
            //click pe bec galben 
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

        }


        public static IWebElement WaitForElementClickable(IWebDriver driver, int seconds, By locator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }


        //METODA 1 folosita pt citirea datelor de test din fisier csv
        //ReadConfig este metoda pt citirea unui fisier extern, de tip text, DE PROPRIETATI (vezi config.properties sau apiconfig.properties)
        //string configFilePath e calea catre fisierul config.properties/apiconfig.properties
        //eu ii dau fisierul de configurare, de tip PROPERTIES
        //metoda va citi config.properties + va returna un Dictionar de tip cheie valoare cu informatiile din config.properties

        public static Dictionary<string, string> ReadConfig(string configFilePath)
        {
            //cream Dictionarul de tip string,string
            var configData = new Dictionary<string, string>();

            //citeste fiecare linie din fisierul text 
            foreach (var line in File.ReadAllLines(configFilePath))
            {
                //pt fiecare linie din fisier, adaugam in Dictionar: valoarea dinainte de =, valoarea de dupa =
                //am folosit Trim() pt a elimina cazurile cand in config.properties se pune spatiu inainte sau dupa egal
                string[] values = line.Split('=');
                configData.Add(values[0].Trim(), values[1].Trim());
            }

            //metoda va citi fisierul de configurare si va returna un Dictionar de tip cheie valoare cu informatiile din acel fisier
            return configData;
        }




//metoda pt a face screenshot pt test report
        //pt linia de mai jos, click pe beculetul galben si selectez "using AventStack.ExtentReports;"
        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string name)
        {
            var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, name).Build();
        }

    }
}
