using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishPoint.Pages
{
    public class MatchingEnginePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // Locators
        private readonly By modulesLink = By.XPath("//a[normalize-space(text())='Modules']");
        private readonly By repertoireLink = By.XPath("//a[normalize-space(text())='Repertoire Management Module']");
        private readonly By additionalFeaturesHeading = By.XPath("//h2[normalize-space(text())='Additional Features']");
        private readonly By productsSupportedTab = By.XPath("//span[normalize-space(text())='Products Supported']");
        private readonly By productsHeading = By.XPath("//h3[normalize-space()='There are several types of Product Supported:']");
        private readonly By productListContainer = By.XPath("//h3[normalize-space()='There are several types of Product Supported:']/following-sibling::div//ul");

        // Constructor

        public MatchingEnginePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        //  Actions
        public void ClickModules()
        {
            wait.Until(d => d.FindElement(modulesLink)).Click();
        }

        public void ClickRepertoireManagement()
        {
            wait.Until(d => d.FindElement(repertoireLink)).Click();
        }

        public void ScrollToAdditionalFeatures()
        {
            var element = wait.Until(d => d.FindElement(additionalFeaturesHeading));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ClickProductsSupported()
        {
            wait.Until(d => d.FindElement(productsSupportedTab)).Click();
        }

        public void ScrollToProductsHeading()
        {
            var element = wait.Until(d => d.FindElement(productsHeading));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public List<string> GetProductList()
        {
            var listContainer = wait.Until(d => d.FindElement(productListContainer));
            return listContainer.FindElements(By.TagName("li"))
                                .Select(li => li.Text.Trim())
                                .ToList();
        }
    }
}
