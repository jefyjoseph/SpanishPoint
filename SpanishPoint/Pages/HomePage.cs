using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishPoint.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private IConfiguration config;

        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory()) // or specify your path
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            config = builder.Build();
            this.driver = driver;
            this.wait = wait;
        }

        public void OpenUrl()
        {
            //string url = ConfigurationManager.AppSettings["BaseUrl"];
            string url = config["BaseUrl"];
            Console.WriteLine($"URL from config: {url}");

            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("BaseUrl is not configured properly!");
            }
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

    }
}
