using System;
using System.Collections.Generic;
using System.Text;

namespace connectToBD_EntityFramework.Utilities
{
   public class FrameworkConstants
    {

        //e o cale relativa care nu se va modifica in functie de mediul de testare (UAT/PROD)
        const string extensionPath = "Other\\ExtensionFile";



//citesc config.properties de pe disk
       static Dictionary<string, string> configData = Utils.ReadConfig("config.properties");
      //protocol e citit din Dictionarul configData
      static string protocol = configData["protocol"];
      static string hostname = configData["hostname"];
     // static string port = configData["port"];
    //  static string path = configData["apppath"];

        public static string browserProxy = configData["proxyserver"];

   //configData["headless"] va returna un string => pt ca startHeadless sa fie boolean, voi face Parse 
   //public static bool startHeadless = Boolean.Parse(configData["headless"]);
        public static bool startHeadless = GetHeadlessConfig();
        public static bool useProxy = Boolean.Parse(configData["useproxy"]);
        public static bool startMaximized = Boolean.Parse(configData["startmaximized"]);
        public static bool ignoreCertificate = Boolean.Parse(configData["ignorecerterr"]);
        public static bool startWithExtension = Boolean.Parse(configData["extension"]);

        public static string configBrowser = configData["browser"];




//metoda statica pt a returna url-ul 
        public static string GetUrl()
        {
            //return String.Format("{0}://{1}:{2}{3}",protocol,hostname,port,path);
            return String.Format("{0}://{1}", protocol, hostname);
        }





//metoda statica pt a retura numele unei extensii
        //voi apela metoda cand vreau sa rulez testele cu browser cu extensie instalata
        public static string GetExtensionName(WebBrowsers browserType)
        {
            switch(browserType)
            {
                case WebBrowsers.Firefox:
                    {
                        return String.Format("{0}\\metamask-10.8.1-an+fx.xpi", extensionPath);
                    }

                default:
                    {
                        return String.Format("{0}\\extension_4_42_0_0.crx", extensionPath);
                    }
            }
        }




    


//metoda pt rularea testelor cu optiunea driver headless in functie de variabilele de mediu 
        //pas 1: in Cntrol Panel/System and Security/System/Advanced system settings: dau click pe Environment Variables
        //pas 2: cum creez o variabila de mediu: click pe new: si creez variable name = env; variable value = local => click pe OK

        private static bool GetHeadlessConfig()
        {
            //variabilele de mediu sunt intr-o clasa numita Environment (care contine un dictionar: cheie = valoare) -> aici este metoda GetEnvironmentVariables()
            //pas 1: citim dictionarul care contine variabilele
            foreach(string key in Environment.GetEnvironmentVariables().Keys)
            {
                //daca gaseste variabila de mediu "env"
                if(key.Equals("env"))
                {
                    //daca sumplimentar variabila de mediu nu e pe local: va returna headless
                    if (!Environment.GetEnvironmentVariables()[key].Equals("local"))
                    {
                        Console.WriteLine("Found env variable other than local");
                        return true;
                    }

                    //daca gaseste "env", dar valoarea = local: va returna fara headless
                    else
                    {
                        Console.WriteLine("Found env variable for local");
                        return false;
                    }
                }
                //afisam numele si valoarea fiecarei variabile de mediu
            //    Console.WriteLine("Key {0}, Value {1}",key, Environment.GetEnvironmentVariables()[key]);
            }
            //daca nu este variabila de mediu "env", atunci se va rula cu optiunea headless din fisierul config.properties
            Console.WriteLine("No env variable found => read from file config.properties");
            return Boolean.Parse(configData["headless"]);
        }
            
            
     }
}
