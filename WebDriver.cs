﻿using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Security;

namespace WFMarketTool
{
    public class WebDriver
    {
        private IWebDriver driver;

        public WebDriver()
        {
            this.driver = new FirefoxDriver();

            // Driver must wait because it is much faster than the browser. Trying to get an element that isn't there will be hard.
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
        }

        public void StartDriver()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement button = (IWebElement) jsExecutor.ExecuteScript("return document.querySelector(\".btn.btn__primary--L8HyD\")");
            button.Click();

            Console.ReadLine();

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
                    string jsonString = File.ReadAllText("Credentials.json");
                    Credentials? credentials = JsonConvert.DeserializeObject<Credentials>(jsonString);

                    if (credentials != null)
                    {
                        Console.WriteLine($"Password {credentials.Password}\nEmail {credentials.Email}");
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
    }
}
