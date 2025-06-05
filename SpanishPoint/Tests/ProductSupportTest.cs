using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpanishPoint.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace SpanishPoint.Tests
{
    public class ProductSupportTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HomePage homePage;
        private MatchingEnginePage matchingEngine;
        
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            homePage = new HomePage(driver, wait);
            matchingEngine = new MatchingEnginePage(driver, wait);

            homePage.OpenUrl();
        }

        [Test]
        public void ValidateSupportedProductsList()
        {
            matchingEngine.ClickModules();
            matchingEngine.ClickRepertoireManagement();
            matchingEngine.ScrollToAdditionalFeatures();
            Thread.Sleep(1000); // Optional
            matchingEngine.ClickProductsSupported();
            matchingEngine.ScrollToProductsHeading();

            var actualProducts = matchingEngine.GetProductList();

            Console.WriteLine("Products listed on the page:");
            foreach (var product in actualProducts)
                Console.WriteLine($"- {product}");

            var expectedProducts = new List<string>
            {
                "Cue Sheet / AV Work",
                "Recording",
                "Bundle",
                "Advertisement"
            };

            bool match = expectedProducts.SequenceEqual(actualProducts);

            if (!match)
            {
                Console.WriteLine("❌ Assertion Failed: Product list mismatch.");
                Console.WriteLine("Expected:");
                expectedProducts.ForEach(p => Console.WriteLine($"- {p}"));
                Console.WriteLine("Actual:");
                actualProducts.ForEach(p => Console.WriteLine($"- {p}"));
            }
            else
            {
                Console.WriteLine("✅ Assertion Passed: Product list matches expected.");
            }

            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                Thread.Sleep(5000); // Optional: observe browser before closing
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
