using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.PageModels.POM
{
    public class BasePage
    {
        public IWebDriver driver;
      



        //creez constructorul cu parametru driver
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
