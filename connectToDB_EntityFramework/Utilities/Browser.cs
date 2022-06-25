using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.Utilities
{
    public class Browser
    {
        //generam browserul printr-o metoda statica, driverul fiind luat din metoda enum WebBrowser (ultima metoda din clasa asta)
        //de ce static: ocupa mai putina memorie
        
        //optiunile browserului vor fi similare indiferent de browser - NU prea exista cazuri cand fiecare test sa aiba alte optiuni 

        public static IWebDriver GetDriver(WebBrowsers browserType)
        {
            switch (browserType)
            {
                case WebBrowsers.Chrome:
                    {
                        //cream un obiect numit options, de tip ChromeOptions, pt a adauga optiuni la driver
                        var options = new ChromeOptions();


                        //adaugam optiuni la browserul Chrome:
                        //fereastra de browser sa fie full size
                        //daca e adevarata conditia din if se va executa codul din {};  daca e falsa, nu se executa condul din {}
                        if (FrameworkConstants.startMaximized)
                        {
                            options.AddArgument("--start-maximized");
                        }
                        

                        // optiunea headlesss: in testare nu se foloseste partea grafica a browserului => testerul NU VEDE browserul deschis
                        //=> testele ruleaza mai repede (economie de timp prin inchiderea si deschiderea browserului)

                        if (FrameworkConstants.startHeadless)
                        {
                            options.AddArgument("headless");
                        }

                        if (FrameworkConstants.ignoreCertificate)
                        {
                            options.AddArgument("ignore-certificate-errors");
                        }



                        //definim un Proxy (nu ne vom contacta la site cu IP-ul PC-ului, ci cu IP-ul Proxy-ului)
                        var proxy = new Proxy
                        {
                            HttpProxy = FrameworkConstants.browserProxy,
                            //NU vrem sa detecteze proxy-ul (l-am facut doar in scop didactic)  
                            IsAutoDetect = false
                        };

                        if (FrameworkConstants.useProxy)
                        {
                            options.Proxy = proxy;
                        }


                        //comentez codul de mai jos pt ca nu vrem sa instalam extensia selectata
                        //la rularea unui test, cu linia de cod de mai jos se va instala automat extensia selectata
                        // options.AddExtension("C:\\Users\\Miha\\Downloads\\extension_4_42_0_0.crx");

                        //instantiez driverul cu options definite mai sus
                        return new ChromeDriver(options);
                    }


                case WebBrowsers.Firefox:
                    {
                       
                        var firefoxOptions = new FirefoxOptions();

                        List<string> optionList = new List<string>();

                        if (FrameworkConstants.startHeadless)
                        {
                            optionList.Add("--headless");
                        }
                       
                        
                        if (FrameworkConstants.ignoreCertificate)
                        {
                            optionList.Add("--ignore-certificate-errors");
                        }


                        firefoxOptions.AddArguments(optionList);

                        //CUM ADAUGAM EXTENSIA LA un browser Firefox
                        //pas 1: definim un profile de Firefox
                        FirefoxProfile fProfile = new FirefoxProfile();
                        //pas 2: downloadam extensia dand cautare pe google -nota: pt browser Firefox, fisierul VA AVEA extensia .xpi
                        //adaugam extensia ca parametru al profilului
                        if (FrameworkConstants.startWithExtension)
                        {
                            fProfile.AddExtension(FrameworkConstants.GetExtensionName(browserType));
                        }

                        //fProfile e adaugat la optiuni
                        firefoxOptions.Profile = fProfile;

                        //optiunile sunt adaugate la browser
                        return new FirefoxDriver(firefoxOptions);
                    }


                case WebBrowsers.Edge:
                    {
                        //pas 1: cream obiectul de optiuni pt browserul Edge
                        var edgeOptions = new EdgeOptions();


                        if (FrameworkConstants.startMaximized)
                        {
                            edgeOptions.AddArgument("--start-maximized");
                        }
                        if (FrameworkConstants.startHeadless)
                        {
                            edgeOptions.AddArgument("headless");
                        }
                        // //pas 2: cu ajutorul browserului edge descarcam extensia de ex Add Block
                        if (FrameworkConstants.startWithExtension)
                        {
                            //pas 3: adaugam extensia la optiuni
                            edgeOptions.AddExtension(FrameworkConstants.GetExtensionName(browserType));
                        }

                        //driverul pt Edge preia optiunile definite mai sus
                        return new EdgeDriver(edgeOptions);
                    }

                default:
                    {
                        throw new BrowserTypeException(browserType.ToString());
                    }
            }
        }



  //method overloading dar FARA PARAMETRU (pt ca testele sa ruleze cu driverul din configuratie)
  //altfel spus: daca imi ceri un browser (fara sa zici exact care browser), metoda va lua driverul din config.properties
  //=> daca nu precizez ce driver, testele se vor efectua pe browserul din config


        public static IWebDriver GetDriver()
        {
            //WebBrowsers 
            WebBrowsers cfgBrowser;
            switch (FrameworkConstants.configBrowser.ToUpper())
            {
                case "FIREFOX":
                    {
                        cfgBrowser = WebBrowsers.Firefox;
                        break;
                    }
                case "EDGE":
                    {
                        cfgBrowser = WebBrowsers.Edge;
                        break;
                    }
                case "CHROME":
                    {
                        cfgBrowser = WebBrowsers.Chrome;
                        break;
                    }
                default:
                    {
                        throw new BrowserTypeException(String.Format("Browser {0} not supported", FrameworkConstants.configBrowser));
                    }

            }
            //!!!driverul returnat va avea insa optiunile din metoda GetDriver CU PARAMETRII
            return GetDriver(cfgBrowser);
        }

    }



   




        public enum WebBrowsers
            {
                Chrome, 
                Firefox,
                Edge
            }






}
