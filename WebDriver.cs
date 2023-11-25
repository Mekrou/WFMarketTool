using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Security;

namespace WFMarketTool
{
    public class WebDriver
    {
        private IWebDriver driver;
        private bool isLoggedIn { get; set; }

        public WebDriver()
        {
            this.driver = new FirefoxDriver();
            this.isLoggedIn = false;

            // Driver must wait because it is much faster than the browser. Trying to get an element that isn't there will be hard.
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
        }

        public void StartDriver()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement button = (IWebElement) jsExecutor.ExecuteScript("return document.querySelector(\".btn.btn__primary--L8HyD\")");
            button.Click();

            Console.ReadLine();

            // At some point, this will break..
            jsExecutor.ExecuteScript("document.querySelector(\".btn.btn__primary--L8HyD\").click()");
            

            Console.ReadLine();

            driver.Close();
        }

        public void Login()
        {
            driver.Navigate().GoToUrl("https://warframe.market/auth/signin");

            IWebElement emailInput = driver.FindElement(By.Id("LoginEmail"));
            IWebElement passInput = driver.FindElement(By.Id("LoginPassword"));

            // Check if Credentials.json exists. If it does, lets grab email from there.

            if (File.Exists("Credentials.json"))
            {
                try
                {
                   

                    if (Credentials.email != null)
                    {
                        Console.WriteLine($"Password {Credentials.password}\nEmail {Credentials.email}");
                        emailInput.SendKeys(Credentials.email);
                        passInput.SendKeys(Credentials.password);
                        PressLoginButton();
                        isLoggedIn = true;
                    }
                    else
                    {
                        Console.WriteLine("Deserealization failed. Credentials obj is null");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else // Credentials need to be entered and optionally stored.
            {
                Console.WriteLine("Please enter your WFMarket email address: ");
                string? email = Console.ReadLine();

                Console.WriteLine("Please enter your WFMarket password: ");
                string? pass = Console.ReadLine();

                emailInput.SendKeys(email);
                passInput.SendKeys(pass);
            }
        }

        private void PressLoginButton()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement button = (IWebElement)jsExecutor.ExecuteScript("return document.querySelector(\".btn.btn__primary--L8HyD\")");
            button.Click();
            // At some point, this will break..
            jsExecutor.ExecuteScript("document.querySelector(\".btn.btn__primary--L8HyD\").click()");
        }
    }
}
